using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.EntityConfigurations;

public class WalletConfiguration : IEntityTypeConfiguration<WalletEntity>
{
    public void Configure(EntityTypeBuilder<WalletEntity> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<WalletEntity>(x => x.UserId);
    }
}