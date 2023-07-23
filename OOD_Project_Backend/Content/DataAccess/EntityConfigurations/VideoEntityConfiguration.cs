using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class VideoEntityConfiguration : IEntityTypeConfiguration<VideoEntity>
{
    public void Configure(EntityTypeBuilder<VideoEntity> builder)
    {
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<VideoEntity>(x => x.FileId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<VideoEntity>(x => x.ContentId);
        
    }
}