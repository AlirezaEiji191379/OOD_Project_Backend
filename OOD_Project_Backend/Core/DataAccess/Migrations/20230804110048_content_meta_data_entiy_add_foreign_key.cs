using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class content_meta_data_entiy_add_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "ContentMetaDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentMetaDatas_Channels_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentMetaDatas_Channels_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "ContentMetaDatas");
        }
    }
}
