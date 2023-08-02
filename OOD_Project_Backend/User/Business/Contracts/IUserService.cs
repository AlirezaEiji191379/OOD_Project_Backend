using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Business.Contracts;

public interface IUserService
{
    Task<Response> Register(RegisterRequest register);
    Task<Response> Login(LoginRequest loginRequest);
}