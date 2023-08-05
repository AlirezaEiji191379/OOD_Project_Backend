using System.Text;
using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class TextModel : IContentModel
{
    private readonly ITextEntityRepository _textEntityRepository;

    public TextModel(ITextEntityRepository textEntityRepository)
    {
        _textEntityRepository = textEntityRepository;
    }

    public ContentType ContentType => ContentType.Text;
    public Task<FileResult> ShowPreview(int contentId)
    {
        throw new NotImplementedException();
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
        await _textEntityRepository.SaveChangesAsync();
    }
}