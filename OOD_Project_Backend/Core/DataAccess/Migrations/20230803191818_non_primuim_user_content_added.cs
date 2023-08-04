using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class non_primuim_user_content_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonPremiumUsersPremiumContents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPremiumUsersPremiumContents", x => new { x.UserId, x.ContentId });
                    table.ForeignKey(
                        name: "FK_NonPremiumUsersPremiumContents_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NonPremiumUsersPremiumContents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonPremiumUsersPremiumContents_ContentId",
                table: "NonPremiumUsersPremiumContents",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonPremiumUsersPremiumContents");
        }
    }
}
