using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Channel.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.DataAccess.EntityConfigurations;

public class NonPremiumUsersPremiumContentsConfiguration : IEntityTypeConfiguration<NonPremiumUsersPremiumContentsEntity>
{
    public void Configure(EntityTypeBuilder<NonPremiumUsersPremiumContentsEntity> builder)
    {
        builder.HasKey(key => new { key.UserId, key.ContentId });
    }
}