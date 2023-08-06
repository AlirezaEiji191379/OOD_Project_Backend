using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quality)
            .HasConversion(t => t.ToString(),
                t => (FileQuality)Enum.Parse(typeof(FileQuality), t));
    }
}