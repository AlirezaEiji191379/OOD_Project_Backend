using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class InteractionEntityConfiguration : IEntityTypeConfiguration<InteractionEntity>
{
    public void Configure(EntityTypeBuilder<InteractionEntity> builder)
    {
        builder.HasKey(x => new { x.ContentId, x.UserId });
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId);
    }
}