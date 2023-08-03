using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.DataAccess.EntityConfigurations;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ChannelEntity)
            .WithOne()
            .HasForeignKey<SubscriptionEntity>(x => x.ChannelId);
    }
}