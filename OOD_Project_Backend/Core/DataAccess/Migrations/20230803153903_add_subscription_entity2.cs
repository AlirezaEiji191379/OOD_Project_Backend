using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class add_subscription_entity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionEntity_Channels_ChannelId",
                table: "SubscriptionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionEntity",
                table: "SubscriptionEntity");

            migrationBuilder.RenameTable(
                name: "SubscriptionEntity",
                newName: "Subscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionEntity_ChannelId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_ChannelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Channels_ChannelId",
                table: "Subscriptions",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Channels_ChannelId",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "SubscriptionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_ChannelId",
                table: "SubscriptionEntity",
                newName: "IX_SubscriptionEntity_ChannelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionEntity",
                table: "SubscriptionEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionEntity_Channels_ChannelId",
                table: "SubscriptionEntity",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
