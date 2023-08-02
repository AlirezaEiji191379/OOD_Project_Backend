using OOD_Project_Backend.Content.Business.Abstractions;
using OOD_Project_Backend.Content.Business.Services;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Content.DataAccess.Repository.Abstractions;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.DiManager;

public class ContentDiManager : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<ContentService,DefaultContentService>();
        services.AddScoped<IContentRepository,ContentRepository>();
        services.AddScoped<IBaseRepository<ContentMetaDataEntity>,ContentMetaDataRepository>();
    }
}