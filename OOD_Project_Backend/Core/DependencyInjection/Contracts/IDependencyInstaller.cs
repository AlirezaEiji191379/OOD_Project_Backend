namespace OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions
{
    internal interface IDependencyInstaller
    {
        void Install(IServiceCollection services);
    }
}
