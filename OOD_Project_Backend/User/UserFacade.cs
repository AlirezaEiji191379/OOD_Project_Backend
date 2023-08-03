using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.User
{
    public class UserFacade : IUserFacade
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserFacade(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            return _authenticationService.GetCurrentUserId(_httpContextAccessor.HttpContext);
        }
    }
}
