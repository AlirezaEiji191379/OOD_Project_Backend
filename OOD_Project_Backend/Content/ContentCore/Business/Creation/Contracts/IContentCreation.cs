using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Contracts;

public interface IContentCreation
{
    Task<int> Generate(ContentCreationRequest request);
    
    Task UpdateContent(ContentUpdateRequest updateRequest);
}