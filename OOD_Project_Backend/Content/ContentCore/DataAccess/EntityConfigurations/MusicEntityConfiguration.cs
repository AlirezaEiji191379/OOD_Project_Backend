using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class MusicEntityConfiguration : IEntityTypeConfiguration<MusicEntity>
{
    public void Configure(EntityTypeBuilder<MusicEntity> builder)
    {
        builder.HasKey(x => x.ContentId);
        builder.HasAlternateKey(x => x.FileId);
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<MusicEntity>(x => x.FileId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<MusicEntity>(x => x.ContentId);
    }
}