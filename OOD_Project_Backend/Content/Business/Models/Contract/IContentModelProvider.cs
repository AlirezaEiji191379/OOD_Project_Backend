using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.Business.Models.Contract;

public interface IContentModelProvider
{
    IContentModel GetContentModel(ContentType contentType);
}