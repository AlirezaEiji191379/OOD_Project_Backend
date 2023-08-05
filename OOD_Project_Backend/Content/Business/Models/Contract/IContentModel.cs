using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models.Contract;

public interface IContentModel
{
    ContentType ContentType { get; }
    Task<FileResult> ShowPreview(int contentId);
    Task<FileResult> ShowNormal(int contentId);
    Task Delete(int contentId);
}