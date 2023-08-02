using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.User.Business;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Security;
using OOD_Project_Backend.User.Business.Validation;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repositories;

namespace OOD_Project_Backend.User.DependencyInjection;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBaseRepository<UserEntity>,UserRepository>();
        serviceCollection.AddScoped<IUserService, DefaultUserService>();
        serviceCollection.AddScoped<IPasswordService,DefaultPasswordService>();
        serviceCollection.AddScoped<IAuthenticator, JwtAuthenticator>();
        serviceCollection.AddScoped<IValidator, Validator>();
    }
}