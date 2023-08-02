namespace OOD_Project_Backend.User.Business.Contracts;

public interface IAuthenticator
{
    string GenerateToken(int userId);
    int FindUserId(string token);
    bool ValidateToken(string token);
}