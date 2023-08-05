using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;

namespace OOD_Project_Backend.Content.Business.Creation;

public interface IContentCreation
{
    Task<int> Generate(ContentCreationRequest request);
}