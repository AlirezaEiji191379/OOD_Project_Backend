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
        services.AddScoped<IContentService,DefaultContentService>();
        services.AddScoped<IContentRepository,ContentRepository>();
        services.AddScoped<IContentMetaDataRepository,ContentMetaDataRepository>();
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<IFileEntityRepository,FileRepository>();
        services.AddScoped<IMusicRepository,MusicRepository>();
        services.AddScoped<ISubtitleRepository,SubtitleRepository>();
        services.AddScoped<ITextEntityRepository,TextEntityRepository>();
        services.AddScoped<IVideoEntityRepository,VideoRepository>();
        services.AddScoped<IContentFacade, ContentFacade>();
    }
}