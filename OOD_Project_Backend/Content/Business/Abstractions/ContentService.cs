using OOD_Project_Backend.Content.Business.Requests;
using OOD_Project_Backend.Core.Common.Response;

namespace OOD_Project_Backend.Content.Business.Abstractions;

public interface ContentService
{
    Task<Response> Add(ContentCreationRequest request);
}