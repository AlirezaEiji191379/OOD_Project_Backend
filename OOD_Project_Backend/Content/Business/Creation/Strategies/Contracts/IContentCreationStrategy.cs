using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;

public interface IContentCreationStrategy
{
    ContentType ContentType { get; }
    Task Generate(ContentCreationRequest contentCreationRequest,ContentEntity content);
}