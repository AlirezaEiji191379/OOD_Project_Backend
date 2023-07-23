using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.User.Business;
using OOD_Project_Backend.User.Business.Abstractions;
using OOD_Project_Backend.User.Business.Security;
using OOD_Project_Backend.User.Business.Validation;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repository;

namespace OOD_Project_Backend.User.DependencyInjection;

public class UserPackageDiManager : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBaseRepository<UserEntity>,UserRepository>();
        serviceCollection.AddScoped<UserService, DefaultUserService>();
        serviceCollection.AddScoped<PasswordService,DefaultPasswordService>();
        serviceCollection.AddScoped<Authenticator, JwtAuthenticator>();
        //serviceCollection.AddSingleton<Validator,DefaultValidator>();
    }
}