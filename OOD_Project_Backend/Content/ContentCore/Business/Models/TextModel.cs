using System.Text;
using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models;

public class TextModel : IContentModel
{
    private readonly ITextRepository _textRepository;
    private readonly IContentRepository _contentRepository;
    
    public TextModel(ITextRepository textRepository, IContentRepository contentRepository)
    {
        _textRepository = textRepository;
        _contentRepository = contentRepository;
    }

    public ContentType ContentType => ContentType.Text;
    public async Task<FileResult> ShowPreview(int contentId)
    {
        var textEntity = await _textRepository.FindByContentId(contentId);
        byte[] byteArray = Encoding.UTF8.GetBytes(textEntity.Value.Substring(0,4));
        string contentType = "text/plain";
        return new FileContentResult(byteArray,contentType);
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var textEntity = await _textRepository.FindByContentId(contentId);
        byte[] byteArray = Encoding.UTF8.GetBytes(textEntity.Value);
        string contentType = "text/plain";
        return new FileContentResult(byteArray, contentType);
    }

    public async Task Delete(int contentId)
    {
        var textEntity = await _textRepository.FindByContentId(contentId);
        _textRepository.Delete(textEntity);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        await _textRepository.SaveChangesAsync();
    }
}