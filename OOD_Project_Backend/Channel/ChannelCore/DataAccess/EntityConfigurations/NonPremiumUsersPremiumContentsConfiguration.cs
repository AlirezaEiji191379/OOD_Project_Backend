using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.EntityConfigurations;

public class NonPremiumUsersPremiumContentsConfiguration : IEntityTypeConfiguration<NonPremiumUsersPremiumContentsEntity>
{
    public void Configure(EntityTypeBuilder<NonPremiumUsersPremiumContentsEntity> builder)
    {
        builder.HasKey(key => new { key.UserId, key.ContentId });
    }
}