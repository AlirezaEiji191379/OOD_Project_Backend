using MailKit.Net.Smtp;
using MimeKit;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.User.Business.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailToUser(string email, int code)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Ghasedak Team Support", "alirezaeiji191379@gmail.com"));
        message.To.Add(new MailboxAddress("Ghasedak Client", email));
        message.Subject = "Delete Account Verification Email";
        var text = new TextPart("plain")
        {
            Text = $"verification Code is {code} and it is valid for 15 mins!"
        };
        message.Body = text;
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync("alirezaeiji191379@gmail.com", "jvetnhlkdhukjimm");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}