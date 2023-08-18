using Microsoft.EntityFrameworkCore;
using System.Reflection;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities;
using OOD_Project_Backend.Content.Category.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Core.DataAccess
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChannelEntity> Channels { get; set; }
        public DbSet<ChannelMemberEntity> ChannelMembers { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<ContentEntity> Contents { get; set; }
        public DbSet<ContentMetaDataEntity> ContentMetaDatas { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<MusicEntity> Musics { get; set; }
        public DbSet<TextEntity> Texts { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<RefundEntity> Refunds { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }
        public DbSet<NonPremiumUsersPremiumContentsEntity> NonPremiumUsersPremiumContents { get; set; }
        public DbSet<ChannelPremiumUsersEntity> ChannelPremiumUsers { get; set; }
    }
}
