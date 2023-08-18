using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;

public interface IContentCreationStrategy
{
    ContentType ContentType { get; }
    Task Generate(ContentCreationRequest contentCreationRequest,ContentEntity content);
    
    Task UpdateContent(ContentUpdateRequest updateRequest);
}