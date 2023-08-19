using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DependencyInjection.Contracts;
using OOD_Project_Backend.Core.Job;
using StackExchange.Redis;

namespace OOD_Project_Backend.Core.DependencyInjection
{
    internal static class DiManager
    {
        internal static void AddOODServices(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            var installers = GetAllIDependencyInstallerImplementations();
            foreach (var installer in installers)
            {
                installer.Install(serviceCollection);
            }

            serviceCollection.AddDbContext<AppDbContext>
                    (options =>
                        options.UseNpgsql(configuration.GetConnectionString("GhasedakDb")));
            serviceCollection.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString")));
           // serviceCollection.AddHostedService<JobsRunner>();
        }

        private static IEnumerable<IDependencyInstaller> GetAllIDependencyInstallerImplementations()
        {
            return typeof(IAssemblyMarkerInterface)
                   .Assembly
                   .DefinedTypes
                   .Where(type => !type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(IDependencyInstaller)))
                   .Select(Activator.CreateInstance)
                   .Cast<IDependencyInstaller>();
        }

    }
}
