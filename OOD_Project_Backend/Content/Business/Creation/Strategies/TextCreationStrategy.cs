using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies;

public class TextCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Text;
    private readonly ITextEntityRepository _textEntityRepository;

    public TextCreationStrategy(ITextEntityRepository textEntityRepository)
    {
        _textEntityRepository = textEntityRepository;
    }

    public async Task Generate(ContentCreationRequest request,ContentEntity content)
    {
        var textEntity = new TextEntity()
        {
            Content = content,
            Value = request.Value
        };
        await _textEntityRepository.Create(textEntity);
    }
}