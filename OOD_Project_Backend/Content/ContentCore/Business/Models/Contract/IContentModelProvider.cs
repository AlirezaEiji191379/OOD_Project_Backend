using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;

public interface IContentModelProvider
{
    IContentModel GetContentModel(ContentType contentType);
}