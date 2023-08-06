using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models.Provider;

public class ContentModelProvider : IContentModelProvider
{
    private readonly IDictionary<ContentType, IContentModel> _contentModelsByContentType;

    public ContentModelProvider(IEnumerable<IContentModel> contentModels)
    {
        _contentModelsByContentType = contentModels.ToDictionary(x => x.ContentType);
    }

    public IContentModel GetContentModel(ContentType contentType)
    {
        return _contentModelsByContentType[contentType];
    }
}