using Microsoft.EntityFrameworkCore;

namespace OOD_Project_Backend.Core.DataAccess;

public class Migrator
{
    public static void Migrate(WebApplication app)
    {
        using (var service = app.Services.CreateScope())
        {
            Console.WriteLine("Appling migrations to the Database...");
            service.ServiceProvider.GetService<AppDbContext>()?.Database.Migrate();
        }
    }
}