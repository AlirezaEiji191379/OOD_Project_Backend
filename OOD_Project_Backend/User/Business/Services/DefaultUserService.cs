using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;
using OOD_Project_Backend.User.Business.Validations.Conditions;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;

namespace OOD_Project_Backend.User.Business.Services;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator _validator;
    private readonly IPasswordService _passwordService;
    private readonly IAuthenticator _jwtAuthenticator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IFinanceFacade _financeFacade;
    public DefaultUserService(IUserRepository userRepository,
        IPasswordService passwordService,
        IAuthenticator jwtAuthenticator, 
        IFinanceFacade financeFacade,
        ITokenRepository tokenRepository, 
        IValidator validator)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtAuthenticator = jwtAuthenticator;
        _financeFacade = financeFacade;
        _tokenRepository = tokenRepository;
        _validator = validator;
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
        await using var transaction = await _userRepository.BeginTransactionAsync();
        try
        {
            await _userRepository.Create(user);
            await _userRepository.SaveChangesAsync();
            await _financeFacade.CreateWallet(user.Id);
            await _userRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response(201, new { Message = "User Created" });
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new Response(400, new { Message = "duplicated user is found or the sign up failed!" });
        }
    }

    public async Task<Response> Login(LoginRequest loginRequest)
    {
        try
        {
            var user = string.IsNullOrEmpty(loginRequest.PhoneNumber)
                ? await _userRepository.FindByCondition(x => x.Email == loginRequest.Email && x.IsDeleted == false, false).FirstAsync()
                : await _userRepository.FindByCondition(x => x.PhoneNumber == loginRequest.PhoneNumber && x.IsDeleted == false, false)
                    .FirstAsync();
            if (!_passwordService.VerifyPassword(loginRequest.Password, user.Password))
            {
                return new Response(404, new { Message = "invalid email/phone and password" });
            }

            var jwtToken = _jwtAuthenticator.GenerateToken(user.Id);
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
            var jti = _jwtAuthenticator.FindJwtId(httpContext.Request.Headers["X-Auth-Token"].FirstOrDefault());
            await _tokenRepository.SaveBlackListedTokenId(jti);
            return new Response(200, new { Message = "logout succesfull!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "the logout failed!" });
        }
    }
}