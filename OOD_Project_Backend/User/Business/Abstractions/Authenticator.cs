namespace OOD_Project_Backend.User.Business.Abstractions;

public interface Authenticator
{
    string GenerateToken(int userId);
}