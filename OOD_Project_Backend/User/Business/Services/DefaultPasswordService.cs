using Microsoft.AspNetCore.Identity;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.User.Business.Security;

public class DefaultPasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<string>();
        return passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var passwordHasher = new PasswordHasher<string>();
        var verifyResult = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return verifyResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}