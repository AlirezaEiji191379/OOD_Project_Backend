using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.EntityConfigurations;

public class ContentEntityConfiguration : IEntityTypeConfiguration<ContentEntity>
{
    public void Configure(EntityTypeBuilder<ContentEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}