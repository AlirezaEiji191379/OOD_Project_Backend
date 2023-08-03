using Microsoft.EntityFrameworkCore;

namespace OOD_Project_Backend.Core.DataAccess;

public class Migrator
{
    public static async Task Migrate(WebApplication app)
    {
        using (var service = app.Services.CreateScope())
        {

            var appDbContext = service.ServiceProvider.GetService<AppDbContext>();
            var pendingMigrations = await appDbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Console.WriteLine("Applying pending migrations to the Database...");
                await appDbContext.Database.MigrateAsync();
            }
            Console.WriteLine("Database Migrations Are Up to date!");
        }
    }
}