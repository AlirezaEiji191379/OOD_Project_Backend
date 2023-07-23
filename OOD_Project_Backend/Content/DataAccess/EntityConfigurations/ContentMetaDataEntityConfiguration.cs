using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class ContentMetaDataEntityConfiguration : IEntityTypeConfiguration<ContentMetaDataEntity>
{
    public void Configure(EntityTypeBuilder<ContentMetaDataEntity> builder)
    {
        builder.HasOne(x => x.Content)
            .WithOne(x => x.ContentMetaData)
            .HasForeignKey<ContentMetaDataEntity>(x => x.ContentId);
    }
}