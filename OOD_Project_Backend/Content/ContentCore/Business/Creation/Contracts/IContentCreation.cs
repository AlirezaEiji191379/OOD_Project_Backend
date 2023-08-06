using OOD_Project_Backend.Content.ContentCore.Business.Contexts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Contracts;

public interface IContentCreation
{
    Task<int> Generate(ContentCreationRequest request);
}