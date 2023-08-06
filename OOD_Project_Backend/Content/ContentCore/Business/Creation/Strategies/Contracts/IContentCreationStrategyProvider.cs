using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;

public interface IContentCreationStrategyProvider
{
    IContentCreationStrategy GetContentCreationStrategy(ContentType contentType);
}