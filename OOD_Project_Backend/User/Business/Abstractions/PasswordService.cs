namespace OOD_Project_Backend.User.Business.Abstractions;

public interface PasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password,string hashedPassword);
}