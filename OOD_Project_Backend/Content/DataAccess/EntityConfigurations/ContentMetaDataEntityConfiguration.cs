using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class ContentMetaDataEntityConfiguration : IEntityTypeConfiguration<ContentMetaDataEntity>
{
    public void Configure(EntityTypeBuilder<ContentMetaDataEntity> builder)
    {
        builder.HasKey(x => x.ContentId);
        builder.HasOne(x => x.Content)
            .WithOne()
            .HasForeignKey<ContentMetaDataEntity>(x => x.ContentId);
        builder.HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey(x => x.ChannelId);
    }
}