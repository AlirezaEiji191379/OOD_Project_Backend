using OOD_Project_Backend.Content.Business.Abstractions;
using OOD_Project_Backend.Content.Business.Services;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Abstractions;

namespace OOD_Project_Backend.Content.DiManager;

public class ContentDiManager : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ContentService,DefaultContentService>();
        serviceCollection.AddScoped<IBaseRepository<ContentEntity>,ContentRepository>();
        serviceCollection.AddScoped<IBaseRepository<ContentMetaDataEntity>,ContentMetaDataRepository>();
    }
}