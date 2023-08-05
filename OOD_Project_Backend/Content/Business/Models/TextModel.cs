using System.Text;
using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class TextModel : IContentModel
{
    private readonly ITextEntityRepository _textEntityRepository;
    private readonly IContentRepository _contentRepository;
    
    public TextModel(ITextEntityRepository textEntityRepository, IContentRepository contentRepository)
    {
        _textEntityRepository = textEntityRepository;
        _contentRepository = contentRepository;
    }

    public ContentType ContentType => ContentType.Text;
    public async Task<FileResult> ShowPreview(int contentId)
    {
        var textEntity = await _textEntityRepository.FindByContentId(contentId);
        byte[] byteArray = Encoding.UTF8.GetBytes(textEntity.Value.Substring(0,4));
        string contentType = "text/plain";
        return new FileContentResult(byteArray,contentType);
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var textEntity = await _textEntityRepository.FindByContentId(contentId);
        byte[] byteArray = Encoding.UTF8.GetBytes(textEntity.Value);
        string contentType = "text/plain";
        return new FileContentResult(byteArray, contentType);
    }

    public async Task Delete(int contentId)
    {
        var textEntity = await _textEntityRepository.FindByContentId(contentId);
        _textEntityRepository.Delete(textEntity);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        await _textEntityRepository.SaveChangesAsync();
    }
}