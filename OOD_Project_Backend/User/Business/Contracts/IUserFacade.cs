using OOD_Project_Backend.User.Business.Context;

namespace OOD_Project_Backend.User.Business.Contracts
{
    public interface IUserFacade
    {
        int GetCurrentUserId();
        Task<UserContract> GetUser(int userId);
        Task<UserProfile> GetCurrentUser();
    }
}
