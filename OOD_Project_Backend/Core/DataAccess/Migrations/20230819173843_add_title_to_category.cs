using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_title_to_category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CategoryEntities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntities_Title",
                table: "CategoryEntities",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryEntities_Title",
                table: "CategoryEntities");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CategoryEntities");
        }
    }
}
