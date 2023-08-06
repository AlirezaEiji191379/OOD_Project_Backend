using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Models;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Provider;
using OOD_Project_Backend.Content.ContentCore.Business.Services;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;

namespace OOD_Project_Backend.Content;

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
        services.AddScoped<ITextRepository,TextRepository>();
        services.AddScoped<IVideoEntityRepository,VideoRepository>();
        services.AddScoped<IContentFacade, ContentFacade>();
        services.AddScoped<IContentCreationStrategyProvider, ContentCreationStrategyProvider>();
        services.AddScoped<IContentCreationStrategy, MusicCreationStrategy>();
        services.AddScoped<IContentCreationStrategy, VideoCreationStrategy>();
        services.AddScoped<IContentCreationStrategy, TextCreationStrategy>();
        services.AddScoped<IContentCreation, ContentCreation>();
        services.AddScoped<IContentModel,MusicModel>();
        services.AddScoped<IContentModel,VideoModel>();
        services.AddScoped<IContentModel,TextModel>();
        services.AddScoped<IContentModelProvider, ContentModelProvider>();
    }
}