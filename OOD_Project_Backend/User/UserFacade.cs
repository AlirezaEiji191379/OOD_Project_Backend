using OOD_Project_Backend.User.Business.Context;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;

namespace OOD_Project_Backend.User
{
    public class UserFacade : IUserFacade
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        
        public UserFacade(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public int GetCurrentUserId()
        {
            return _authenticationService.GetCurrentUserId(_httpContextAccessor.HttpContext);
        }

        public async Task<UserContract> GetUser(int userId)
        {
            return await _userRepository.GetUserProfile(userId);
        }
    }
}
