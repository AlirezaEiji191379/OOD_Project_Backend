using Microsoft.EntityFrameworkCore;
using System.Reflection;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Core.DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserEntity> Users { get; set; }

    }
}
