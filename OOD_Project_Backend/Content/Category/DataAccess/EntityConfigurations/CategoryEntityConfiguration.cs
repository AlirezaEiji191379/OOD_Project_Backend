using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.Category.DataAccess.Entities;

namespace OOD_Project_Backend.Content.Category.DataAccess.EntityConfigurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Title);
        builder.HasOne(x => x.Channel)
            .WithMany(x => x.CategoryEntities)
            .HasForeignKey(x => x.ChannelId);
    }
}