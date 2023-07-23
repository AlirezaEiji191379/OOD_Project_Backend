namespace OOD_Project_Backend.User.Business.Abstractions;

public interface Authenticator
{
    string GenerateToken(int userId);
    int FindUserId(string token);
    bool ValidateToken(string token);
}