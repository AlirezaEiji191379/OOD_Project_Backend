using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Channel.DataAccess.EntityConfigurations;

public class ChannelMemberConfiguration : IEntityTypeConfiguration<ChannelMemberEntity>
{
    public void Configure(EntityTypeBuilder<ChannelMemberEntity> builder)
    {
        builder.Property(x => x.Role)
            .HasConversion(t => t.ToString(),
                t => (Role)Enum.Parse(typeof(Role), t));
        builder.HasOne(x => x.Channel)
            .WithMany(x => x.ChannelMemberEntities)
            .HasForeignKey(fk => fk.ChannelId);
        builder.HasOne(x => x.User)
            .WithMany(x => x.ChannelMemberEntities)
            .HasForeignKey(fk => fk.UserId);
        builder.HasKey(x => new { x.UserId, x.ChannelId });
    }
}