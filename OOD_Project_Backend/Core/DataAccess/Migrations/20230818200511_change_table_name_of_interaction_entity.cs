using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class change_table_name_of_interaction_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteractionEntity_Contents_ContentId",
                table: "InteractionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionEntity_Users_UserId",
                table: "InteractionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionEntity",
                table: "InteractionEntity");

            migrationBuilder.RenameTable(
                name: "InteractionEntity",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionEntity_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "ContentId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Contents_ContentId",
                table: "Likes",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Contents_ContentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "InteractionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "InteractionEntity",
                newName: "IX_InteractionEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionEntity",
                table: "InteractionEntity",
                columns: new[] { "ContentId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionEntity_Contents_ContentId",
                table: "InteractionEntity",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionEntity_Users_UserId",
                table: "InteractionEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
