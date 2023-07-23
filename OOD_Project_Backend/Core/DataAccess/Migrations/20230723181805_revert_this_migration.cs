using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class revert_this_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentMetaDatas_CategoryEntities_CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentMetaDatas_Channels_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "ContentMetaDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ContentMetaDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "ContentMetaDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_CategoryId",
                table: "ContentMetaDatas",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentMetaDatas_CategoryEntities_CategoryId",
                table: "ContentMetaDatas",
                column: "CategoryId",
                principalTable: "CategoryEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentMetaDatas_Channels_ChannelId",
                table: "ContentMetaDatas",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
