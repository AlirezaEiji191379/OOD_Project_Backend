﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OOD_Project_Backend.Core.DataAccess;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230804112734_remove_content_meta_data_key")]
    partial class remove_content_meta_data_key
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("JoinLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PicturePath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JoinLink")
                        .IsUnique();

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelMemberEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.Property<double>("IncomeShare")
                        .HasColumnType("double precision");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("ChannelMembers");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelPremiumUsersEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("ChannelPremiumUsers");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.NonPremiumUsersPremiumContentsEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "ContentId");

                    b.HasIndex("ContentId");

                    b.ToTable("NonPremiumUsersPremiumContents");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.SubscriptionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("CategoryEntities");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChannelEntityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChannelEntityId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.ContentMetaDataEntity", b =>
                {
                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<int>("ChannelId")
                        .HasColumnType("integer");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Premium")
                        .HasColumnType("boolean");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("ContentId");

                    b.HasIndex("ChannelId");

                    b.ToTable("ContentMetaDatas");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Quality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Size")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.MusicEntity", b =>
                {
                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("MusicText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("ContentId")
                        .IsUnique();

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.VideoEntity", b =>
                {
                    b.Property<int>("ContentId")
                        .HasColumnType("integer");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.HasIndex("ContentId")
                        .IsUnique();

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.RefundEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("BankToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.WalletEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NationalCode")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicPath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelMemberEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", "Channel")
                        .WithMany("ChannelMemberEntities")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithMany("ChannelMemberEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelPremiumUsersEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.NonPremiumUsersPremiumContentsEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.SubscriptionEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", "ChannelEntity")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChannelEntity");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.CategoryEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", "Channel")
                        .WithMany("CategoryEntities")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", null)
                        .WithMany("ContentEntities")
                        .HasForeignKey("ChannelEntityId");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.ContentMetaDataEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", "Content")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Content.DataAccess.Entities.ContentMetaDataEntity", "ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Content");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.MusicEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", "Content")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Content.DataAccess.Entities.MusicEntity", "ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.FileEntity", "File")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Content.DataAccess.Entities.MusicEntity", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("File");
                });

            modelBuilder.Entity("OOD_Project_Backend.Content.DataAccess.Entities.VideoEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.ContentEntity", "Content")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Content.DataAccess.Entities.VideoEntity", "ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.Content.DataAccess.Entities.FileEntity", "File")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Content.DataAccess.Entities.VideoEntity", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("File");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.RefundEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.Finanace.DataAccess.Entities.TransactionEntity", "Transaction")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Finanace.DataAccess.Entities.RefundEntity", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithMany("RefundEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.TransactionEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithMany("TransactionEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Finanace.DataAccess.Entities.WalletEntity", b =>
                {
                    b.HasOne("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", "User")
                        .WithOne()
                        .HasForeignKey("OOD_Project_Backend.Finanace.DataAccess.Entities.WalletEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OOD_Project_Backend.Channel.DataAccess.Entities.ChannelEntity", b =>
                {
                    b.Navigation("CategoryEntities");

                    b.Navigation("ChannelMemberEntities");

                    b.Navigation("ContentEntities");
                });

            modelBuilder.Entity("OOD_Project_Backend.User.DataAccess.Entities.UserEntity", b =>
                {
                    b.Navigation("ChannelMemberEntities");

                    b.Navigation("RefundEntities");

                    b.Navigation("TransactionEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
