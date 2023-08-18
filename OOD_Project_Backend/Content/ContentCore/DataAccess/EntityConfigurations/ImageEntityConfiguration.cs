using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class ImageEntityConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(x => x.ContentId);
        builder.HasAlternateKey(x => x.FileId);
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<ImageEntity>(x => x.FileId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<ImageEntity>(x => x.ContentId);
    }
}