using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.User.Business;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Security;
using OOD_Project_Backend.User.Business.Services;
using OOD_Project_Backend.User.Business.Validation;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repositories;

namespace OOD_Project_Backend.User.DependencyInjection;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<UserEntity>,UserRepository>();
        services.AddScoped<IUserService, DefaultUserService>();
        services.AddScoped<IPasswordService,DefaultPasswordService>();
        services.AddScoped<IAuthenticator, JwtAuthenticator>();
        services.AddScoped<IValidator, Validator>();
        services.AddScoped<IAuthenticationService, DefaultAuthenticationService>();
        services.AddScoped<IUserFacade, UserFacade>();
    }
}