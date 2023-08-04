using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.EntityConfigurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(x => x.TransactionEntities)
            .HasForeignKey(x => x.UserId);
    }
}