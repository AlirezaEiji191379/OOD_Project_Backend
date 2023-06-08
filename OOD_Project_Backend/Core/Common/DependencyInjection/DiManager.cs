using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Common.Assembly;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess;

namespace OOD_Project_Backend.Core.Common.DependencyInjection
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
                        options.UseNpgsql(configuration["GhasedakDb"]));

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
