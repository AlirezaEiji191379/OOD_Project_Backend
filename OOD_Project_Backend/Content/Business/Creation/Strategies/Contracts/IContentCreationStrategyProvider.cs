using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;

public interface IContentCreationStrategyProvider
{
    IContentCreationStrategy GetContentCreationStrategy(ContentType contentType);
}