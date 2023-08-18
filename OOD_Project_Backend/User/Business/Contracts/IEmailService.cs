namespace OOD_Project_Backend.User.Business.Contracts;

public interface IEmailService
{
    Task SendEmailToUser(string email,int code);
}