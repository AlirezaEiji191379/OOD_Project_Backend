using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class update_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ContentMetaDatas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ChannelId",
                table: "Contents",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Channels_ChannelId",
                table: "Contents",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Channels_ChannelId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ChannelId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ContentMetaDatas");
        }
    }
}
