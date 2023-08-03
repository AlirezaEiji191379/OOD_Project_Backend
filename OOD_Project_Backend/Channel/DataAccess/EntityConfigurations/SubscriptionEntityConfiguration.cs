using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Channel.DataAccess.EntityConfigurations;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ChannelEntity)
            .WithOne()
            .HasForeignKey<SubscriptionEntity>(x => x.ChannelId);
        builder.Property(x => x.Period)
            .HasConversion(t => t.ToString(),
                t => (SubscriptionPeriod)Enum.Parse(typeof(SubscriptionPeriod), t));
    }
}