using FluentAssertions;
using NSubstitute;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;
using OOD_Project_Backend.User.Business.Services;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;
using Xunit;

namespace OOD_Project_Backend.Tests.User.Business;

public class DefaultUserServiceTests
{
    private readonly IUserService _sut;
    private readonly IUserRepository _userRepository;
    private readonly IValidator _validator;
    private readonly IPasswordService _passwordService;
    private readonly IAuthenticator _jwtAuthenticator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IFinanceFacade _financeFacade;

    public DefaultUserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _validator = new Validator();
        _passwordService = Substitute.For<IPasswordService>();
        _jwtAuthenticator = Substitute.For<IAuthenticator>();
        _tokenRepository = Substitute.For<ITokenRepository>();
        _financeFacade = Substitute.For<IFinanceFacade>();
        _sut = new DefaultUserService(_userRepository,
            _passwordService,
            _jwtAuthenticator, 
            _financeFacade,
            _tokenRepository, 
            _validator);
    }


    public static TheoryData<RegisterRequest> RegisterRequests()
    {
        return new TheoryData<RegisterRequest>()
        {
            {
                new RegisterRequest()
                {
                    Email = "a.com",
                    Password = "12345"
                }
            },
            {
                new RegisterRequest()
                {
                    PhoneNumber = "09123",
                    Password = "12345"
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(RegisterRequests))]
    public async Task Register_ShouldReturnBadRequest_WhenEmailOrPhoneOrPasswordIsNotValid(RegisterRequest registerRequest)
    {
        // Arrange
        // Act
        var result = await _sut.Register(registerRequest);
        // Assert
        result.StatusCode.Should().Be(400);
    }
    
    
    public static TheoryData<LoginRequest> LoginRequests()
    {
        return new TheoryData<LoginRequest>()
        {
            {
                new LoginRequest()
                {
                    Email = "a.com",
                    Password = "12345"
                }
            },
            {
                new LoginRequest()
                {
                    PhoneNumber = "09123",
                    Password = "12345"
                }
            }
        };
    }
    
    [Theory]
    [MemberData(nameof(LoginRequests))]
    public async Task Login_ShouldReturnNotFound_WhenEmailOrPhoneOrPasswordIsNotValid(LoginRequest loginRequest)
    {
        // Arrange
        // Act
        var result = await _sut.Login(loginRequest);
        // Assert
        result.StatusCode.Should().Be(404);
    }
}