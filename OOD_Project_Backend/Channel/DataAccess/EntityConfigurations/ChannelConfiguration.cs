using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.DataAccess.EntityConfigurations;

public class ChannelConfiguration :  IEntityTypeConfiguration<ChannelEntity>
{
    public void Configure(EntityTypeBuilder<ChannelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.JoinLink).IsUnique();
    }
}