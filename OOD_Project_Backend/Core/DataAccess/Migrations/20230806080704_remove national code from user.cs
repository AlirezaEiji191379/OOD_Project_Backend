using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class removenationalcodefromuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
