using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Business.Abstractions;

public interface UserService
{
    Task<Response> Register(RegisterRequest register);
}