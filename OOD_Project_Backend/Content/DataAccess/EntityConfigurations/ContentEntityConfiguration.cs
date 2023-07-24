﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.EntityConfigurations;

public class ContentEntityConfiguration : IEntityTypeConfiguration<ContentEntity>
{
    public void Configure(EntityTypeBuilder<ContentEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Channel)
            .WithMany(x => x.ContentEntities)
            .HasForeignKey(x => x.ChannelId);
    }
}