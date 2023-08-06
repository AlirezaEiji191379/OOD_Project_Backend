using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class TextEntityConfiguration : IEntityTypeConfiguration<TextEntity>
{
    public void Configure(EntityTypeBuilder<TextEntity> builder)
    {
        builder.HasKey(x => x.ContentId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<TextEntity>(x => x.ContentId);
    }
}