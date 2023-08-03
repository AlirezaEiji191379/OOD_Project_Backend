using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.User.Business.Services;

public class DefaultAuthenticationService : IAuthenticationService
{
    public int GetCurrentUserId(HttpContext httpContext)
    {
        try
        {
            return (int)httpContext.Items["User"];
        }
        catch (Exception e)
        {
            throw new Exception("User is not Authenticated");
        }
    }
}