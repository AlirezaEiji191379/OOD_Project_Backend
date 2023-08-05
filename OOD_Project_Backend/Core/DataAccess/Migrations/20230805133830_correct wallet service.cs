using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class correctwalletservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextEntity_Contents_ContentId",
                table: "TextEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextEntity",
                table: "TextEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wallets");

            migrationBuilder.RenameTable(
                name: "TextEntity",
                newName: "Texts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Texts",
                table: "Texts",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Texts_Contents_ContentId",
                table: "Texts",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Texts_Contents_ContentId",
                table: "Texts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Texts",
                table: "Texts");

            migrationBuilder.RenameTable(
                name: "Texts",
                newName: "TextEntity");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextEntity",
                table: "TextEntity",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextEntity_Contents_ContentId",
                table: "TextEntity",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
