using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Context;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Business.Contracts;

public interface IProfileService
{
    Task<Response> Add(ProfileRequest profileRequest, int userId);
    Task<Response> AddProfilePicture(IFormFile picture, int userId);
    Task<Response> ShowProfile(int userId);
}