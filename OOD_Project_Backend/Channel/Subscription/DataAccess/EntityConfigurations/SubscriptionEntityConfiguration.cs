using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Channel.Subscription.DataAccess.EntityConfigurations;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ChannelEntity)
            .WithMany()
            .HasForeignKey(x => x.ChannelId);
        builder.HasIndex(x => new { x.ChannelId, x.Period }).IsUnique();
        builder.Property(x => x.Period)
            .HasConversion(t => t.ToString(),
                t => (SubscriptionPeriod)Enum.Parse(typeof(SubscriptionPeriod), t));
    }
}