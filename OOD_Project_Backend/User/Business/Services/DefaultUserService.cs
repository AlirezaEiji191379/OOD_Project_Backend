using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finanace.Facade.Abstractions;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;
using OOD_Project_Backend.User.Business.Validations.Conditions;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;

namespace OOD_Project_Backend.User.Business.Services;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IFinanaceFacade _finanaceFacade;
    private readonly IValidator _validator;
    private readonly IPasswordService _passwordService;
    private readonly IAuthenticator _jwtAutenticator;
    private readonly ITokenRepository _tokenRepository;

    public DefaultUserService(IUserRepository userRepository,
        IPasswordService passwordService,
        IAuthenticator jwtAuthenticator, IFinanaceFacade finanaceFacade,
        ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtAutenticator = jwtAuthenticator;
        _finanaceFacade = finanaceFacade;
        _tokenRepository = tokenRepository;
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

    public async Task<Response> Logout(HttpContext httpContext)
    {
        try
        {
            var jti = _jwtAutenticator.FindJwtId(httpContext.Request.Headers["X-Auth-Token"].FirstOrDefault());
            await _tokenRepository.SaveBlackListedTokenId(jti);
            return new Response(200, new { Message = "logout succesfull!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "the logout failed!" });
        }
    }
}