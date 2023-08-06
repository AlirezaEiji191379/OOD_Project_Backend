using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.EntityConfigurations;

public class ChannelPremiumUsersConfiguration : IEntityTypeConfiguration<ChannelPremiumUsersEntity>
{
    public void Configure(EntityTypeBuilder<ChannelPremiumUsersEntity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ChannelId });
    }
}