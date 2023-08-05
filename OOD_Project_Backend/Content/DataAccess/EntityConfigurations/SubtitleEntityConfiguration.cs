using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class SubtitleEntityConfiguration : IEntityTypeConfiguration<SubtitleEntity>
{
    public void Configure(EntityTypeBuilder<SubtitleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.FileId);
        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<SubtitleEntity>(x => x.FileId);
        builder.Property(x => x.Position)
            .HasConversion(t => t.ToString(),
                t => (SubtitlePosition)Enum.Parse(typeof(SubtitlePosition), t));
    }
}