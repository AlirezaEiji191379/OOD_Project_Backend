using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies;

public class ContentCreationStrategyProvider : IContentCreationStrategyProvider
{
    private readonly IDictionary<ContentType, IContentCreationStrategy> _contentCreationStrategiesByContentTypes;
    public ContentCreationStrategyProvider(IEnumerable<IContentCreationStrategy> contentCreationStrategies)
    {
        _contentCreationStrategiesByContentTypes = contentCreationStrategies.ToDictionary(x => x.ContentType);
    }
    
    public IContentCreationStrategy GetContentCreationStrategy(ContentType contentType)
    {
        return _contentCreationStrategiesByContentTypes[contentType];
    }
}