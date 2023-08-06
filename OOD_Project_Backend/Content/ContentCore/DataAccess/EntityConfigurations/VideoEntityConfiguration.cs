using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class VideoEntityConfiguration : IEntityTypeConfiguration<VideoEntity>
{
    public void Configure(EntityTypeBuilder<VideoEntity> builder)
    {
        builder.HasKey(x => x.ContentId);
        builder.HasAlternateKey(x => x.FileId);
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<VideoEntity>(x => x.FileId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<VideoEntity>(x => x.ContentId);
        
    }
}