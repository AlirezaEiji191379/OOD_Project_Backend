using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.EntityConfigurations;

public class RefundConfiguration : IEntityTypeConfiguration<RefundEntity>
{
    public void Configure(EntityTypeBuilder<RefundEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(x => x.RefundEntities)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Transaction)
            .WithOne()
            .HasForeignKey<RefundEntity>(x => x.TransactionId);
    }
}