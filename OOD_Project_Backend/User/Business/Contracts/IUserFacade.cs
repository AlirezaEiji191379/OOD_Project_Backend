namespace OOD_Project_Backend.User.Business.Contracts
{
    public interface IUserFacade
    {
        int GetCurrentUserId(HttpContext httpContext);
    }
}
