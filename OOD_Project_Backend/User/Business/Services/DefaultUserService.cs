using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finanace.Facade.Abstractions;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;
using OOD_Project_Backend.User.Business.Validation;
using OOD_Project_Backend.User.Business.Validation.Rules;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.User.Business;

public class DefaultUserService : IUserService
{
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly IFinanaceFacade _finanaceFacade;
    private readonly IValidator _validator;
    private readonly IPasswordService _passwordService;
    private readonly IAuthenticator _jwtAutenticator;

    public DefaultUserService(IBaseRepository<UserEntity> userRepository,
        IPasswordService passwordService,
        IAuthenticator jwtAuthenticator, IFinanaceFacade finanaceFacade)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtAutenticator = jwtAuthenticator;
        _finanaceFacade = finanaceFacade;
        //TODO : correct the DI for the dependency!
        _validator = new Validator();
    }

    public async Task<Response> Register(RegisterRequest register)
    {
        IRule phoneOrEmailRule = string.IsNullOrEmpty(register.PhoneNumber)
            ? new EmailRule(register.Email)
            : new PhoneNumberRule(register.PhoneNumber);

        var passwordRule = new PasswordRule(register.Password);
        if (!_validator.Validate(phoneOrEmailRule, passwordRule))
        {
            var validateFailedMessage = new
                { Message = "Register Request Is not valid Check Email,Phone and your password" };
            return new Response(400, validateFailedMessage);
        }

        var user = new UserEntity()
        {
            PhoneNumber = register.PhoneNumber,
            Email = register.Email,
            Password = _passwordService.HashPassword(register.Password)
        };
        try
        {
            await _userRepository.Create(user);
            await _finanaceFacade.CreateWallet(user);
            await _userRepository.SaveChangesAsync();
            return new Response(201, new { Message = "User Created" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "duplicated user is found or the sign up failed!" });
        }
    }

    public async Task<Response> Login(LoginRequest loginRequest)
    {
        try
        {
            var user = string.IsNullOrEmpty(loginRequest.PhoneNumber)
                ? await _userRepository.FindByCondition(x => x.Email == loginRequest.Email, false).FirstAsync()
                : await _userRepository.FindByCondition(x => x.PhoneNumber == loginRequest.PhoneNumber, false)
                    .FirstAsync();
            if (!_passwordService.VerifyPassword(loginRequest.Password, user.Password))
            {
                return new Response(404, new { Message = "invalid email/phone and password" });
            }

            var jwtToken = _jwtAutenticator.GenerateToken(user.Id);
            return new Response(200,  new { Message = jwtToken});
        }
        catch (Exception e)
        {
            return new Response(404, new { Message = "invalid email/phone and password" });
        }
    }
}