using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class change_channel_content_meta_data_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId",
                unique: true);
        }
    }
}
