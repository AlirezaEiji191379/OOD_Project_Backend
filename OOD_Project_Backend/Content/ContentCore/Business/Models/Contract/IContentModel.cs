using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;

public interface IContentModel
{
    ContentType ContentType { get; }
    Task<FileResult> ShowPreview(int contentId);
    Task<FileResult> ShowNormal(int contentId);
    Task Delete(int contentId);
}