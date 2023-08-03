using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class add_subscription_change_channelRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ChannelId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ChannelId",
                table: "Subscriptions",
                column: "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ChannelId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ChannelId",
                table: "Subscriptions",
                column: "ChannelId",
                unique: true);
        }
    }
}
