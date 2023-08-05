using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.Business.Models;

public class TextModel : IContentModel
{
    public ContentType ContentType => ContentType.Text;
    public Task<FileResult> ShowPreview(int contentId)
    {
        throw new NotImplementedException();
    }

    public Task<FileResult> ShowNormal(int contentId)
    {
        throw new NotImplementedException();
    }
}