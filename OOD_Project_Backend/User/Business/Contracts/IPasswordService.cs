namespace OOD_Project_Backend.User.Business.Contracts;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password,string hashedPassword);
}