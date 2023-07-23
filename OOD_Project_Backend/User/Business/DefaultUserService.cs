using OOD_Project_Backend.Core.Common.Response;
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

    public DefaultUserService(IBaseRepository<UserEntity> userRepository)
    {
        _userRepository = userRepository;
        //TODO : correct the DI for the dependency!
        _validator = new DefaultValidator();
    }

    public async Task<Response> Register(RegisterRequest register)
    {
        var phoneRule = new PhoneNumberRule(register.PhoneNumber);
        var emailRule = new EmailRule(register.Email);
        var passwordRule = new PasswordRule(register.Password);
        if (!_validator.Validate(phoneRule, emailRule, passwordRule))
        {
            var validateFailedMessage = "Register Request Is not valid Check Email,Phone and your password";
            return new Response( 400,validateFailedMessage);
        }

        var user = new UserEntity()
        {
            PhoneNumber = register.PhoneNumber,
            Email = register.Email,
            Password = register.Password
        };
        try
        {
            await _userRepository.Create(user);
            await _userRepository.SaveChangesAsync();
            return new Response(201, "User Created");
        }
        catch (Exception e)
        {
            return new Response(400, "sign up failed! try later!");
        }
    }
}