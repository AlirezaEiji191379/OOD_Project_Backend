using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Suggestion.Facade.Abstractions;

public interface ITimelineService
{
    Task<Response> Load();
}