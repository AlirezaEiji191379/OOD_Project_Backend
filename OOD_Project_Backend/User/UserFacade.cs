using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.User
{
    public class UserFacade : IUserFacade
    {
        private readonly IAuthenticationService _authenticationService;

        public UserFacade(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            return _authenticationService.GetCurrentUser(httpContext);
        }
    }
}
