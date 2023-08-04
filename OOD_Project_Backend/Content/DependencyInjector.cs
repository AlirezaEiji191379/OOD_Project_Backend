using OOD_Project_Backend.Content.Business.Abstractions;
using OOD_Project_Backend.Content.Business.Contracts;
using OOD_Project_Backend.Content.Business.Services;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.DiManager;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<ContentService,DefaultContentService>();
        services.AddScoped<IContentRepository,ContentRepository>();
        services.AddScoped<IContentMetaDataRepository,ContentMetaDataRepository>();
        services.AddScoped<IContentFacade, ContentFacade>();
    }
}