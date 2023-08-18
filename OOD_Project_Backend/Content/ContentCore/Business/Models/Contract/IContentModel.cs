using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;

public interface IContentModel
{
    ContentType ContentType { get; }
    Task<ShowContentDto> ShowPreview(int contentId);
    Task<ShowContentDto> ShowNormal(int contentId);
    Task Delete(int contentId);
}