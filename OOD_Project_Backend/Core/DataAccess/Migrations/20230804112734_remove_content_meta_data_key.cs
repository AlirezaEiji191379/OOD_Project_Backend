using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class remove_content_meta_data_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Channels_ChannelId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ChannelId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentMetaDatas",
                table: "ContentMetaDatas");

            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_ContentId",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ContentMetaDataId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContentMetaDatas");

            migrationBuilder.AddColumn<int>(
                name: "ChannelEntityId",
                table: "Contents",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentMetaDatas",
                table: "ContentMetaDatas",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ChannelEntityId",
                table: "Contents",
                column: "ChannelEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Channels_ChannelEntityId",
                table: "Contents",
                column: "ChannelEntityId",
                principalTable: "Channels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Channels_ChannelEntityId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ChannelEntityId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentMetaDatas",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "ChannelEntityId",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentMetaDataId",
                table: "Contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContentMetaDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentMetaDatas",
                table: "ContentMetaDatas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ChannelId",
                table: "Contents",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_ContentId",
                table: "ContentMetaDatas",
                column: "ContentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Channels_ChannelId",
                table: "Contents",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
