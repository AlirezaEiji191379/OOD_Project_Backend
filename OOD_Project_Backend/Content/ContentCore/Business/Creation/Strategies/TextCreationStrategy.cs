using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies;

public class TextCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Text;
    private readonly ITextRepository _textRepository;

    public TextCreationStrategy(ITextRepository textRepository)
    {
        _textRepository = textRepository;
    }

    public async Task Generate(ContentCreationRequest request,ContentEntity content)
    {
        var textEntity = new TextEntity()
        {
            Content = content,
            Value = request.Value
        };
        await _textRepository.Create(textEntity);
    }
}