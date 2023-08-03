using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Security;
using OOD_Project_Backend.User.Business.Services;
using OOD_Project_Backend.User.DataAccess.Repositories;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;

namespace OOD_Project_Backend.User;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, DefaultUserService>();
        services.AddScoped<IPasswordService, DefaultPasswordService>();
        services.AddScoped<IAuthenticator, JwtAuthenticator>();
        services.AddScoped<IValidator, Validator>();
        services.AddScoped<IAuthenticationService, DefaultAuthenticationService>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IProfileService, DefaultProfileService>();
    }
}