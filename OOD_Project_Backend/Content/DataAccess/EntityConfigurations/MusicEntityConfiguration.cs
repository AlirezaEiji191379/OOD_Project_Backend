using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class MusicEntityConfiguration : IEntityTypeConfiguration<MusicEntity>
{
    public void Configure(EntityTypeBuilder<MusicEntity> builder)
    {
        builder.HasNoKey();
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<MusicEntity>(x => x.FileId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<MusicEntity>(x => x.ContentId);
    }
}