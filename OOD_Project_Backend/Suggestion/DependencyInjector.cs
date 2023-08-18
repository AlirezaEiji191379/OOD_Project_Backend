using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Suggestion.Business;
using OOD_Project_Backend.Suggestion.Facade.Abstractions;

namespace OOD_Project_Backend.Suggestion;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<ITimelineService, TimelineService>();
    }
}