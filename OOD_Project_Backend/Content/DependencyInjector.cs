using OOD_Project_Backend.Content.Business.Contracts;
using OOD_Project_Backend.Content.Business.Creation;
using OOD_Project_Backend.Content.Business.Creation.Strategies;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.Business.Models;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.Business.Models.Provider;
using OOD_Project_Backend.Content.Business.Services;
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
        services.AddScoped<ITextEntityRepository,TextEntityRepository>();
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