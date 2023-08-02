namespace OOD_Project_Backend.User.Business.Contracts;

public interface IAuthenticationService
{
    int GetCurrentUser(HttpContext httpContext);
}