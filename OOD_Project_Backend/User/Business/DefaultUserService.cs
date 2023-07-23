﻿using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.User.Business.Abstractions;
using OOD_Project_Backend.User.Business.Requests;
using OOD_Project_Backend.User.Business.Validation;
using OOD_Project_Backend.User.Business.Validation.Rules;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.User.Business;

public class DefaultUserService : UserService
{
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly Validator _validator;
    private readonly PasswordService _passwordService;

    public DefaultUserService(IBaseRepository<UserEntity> userRepository, PasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        //TODO : correct the DI for the dependency!
        _validator = new DefaultValidator();
    }

    public async Task<Response> Register(RegisterRequest register)
    {
        Rule phoneOrEmailRule = string.IsNullOrEmpty(register.PhoneNumber)
            ? new EmailRule(register.Email)
            : new PhoneNumberRule(register.PhoneNumber);

        var passwordRule = new PasswordRule(register.Password);
        if (!_validator.Validate(phoneOrEmailRule, passwordRule))
        {
            var validateFailedMessage = new {Message = "Register Request Is not valid Check Email,Phone and your password"};
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
            await _userRepository.SaveChangesAsync();
            return new Response(201, new {Message = "User Created"});
        }
        catch (Exception e)
        {
            return new Response(400, new {Message = "duplicated user is found or the sign up failed!"});
        }
    }
}