using OOD_Project_Backend.Core.Common.Assemb_y;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using System.Reflection;

namespace OOD_Project_Backend.Core.Common.DependencyInjection
{
    internal static class DiManager
    {
        internal static void AddOODServices(this IServiceCollection serviceCollection)
        {
            var installers = GetAllIDependencyInstallerImplementations();
            foreach (var installer in installers)
            {
                installer.Install(serviceCollection);
            }
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
