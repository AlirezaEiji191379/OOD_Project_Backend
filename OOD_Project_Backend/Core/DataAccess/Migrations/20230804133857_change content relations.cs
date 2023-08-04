using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OOD_Project_Backend.Core.DataAccess.Migrations
{
    public partial class changecontentrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Videos_ContentId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_FileId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Musics_ContentId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_FileId",
                table: "Musics");

            migrationBuilder.AddColumn<int>(
                name: "SubtitleId",
                table: "Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ContentMetaDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Videos_FileId",
                table: "Videos",
                column: "FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "ContentId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Musics_FileId",
                table: "Musics",
                column: "FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musics",
                table: "Musics",
                column: "ContentId");

            migrationBuilder.CreateTable(
                name: "SubtitleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtitleEntity", x => x.Id);
                    table.UniqueConstraint("AK_SubtitleEntity_FileId", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_SubtitleEntity_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextEntity",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextEntity", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_TextEntity_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SubtitleId",
                table: "Videos",
                column: "SubtitleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetaDatas_CategoryId",
                table: "ContentMetaDatas",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentMetaDatas_CategoryEntities_CategoryId",
                table: "ContentMetaDatas",
                column: "CategoryId",
                principalTable: "CategoryEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_SubtitleEntity_SubtitleId",
                table: "Videos",
                column: "SubtitleId",
                principalTable: "SubtitleEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentMetaDatas_CategoryEntities_CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_SubtitleEntity_SubtitleId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "SubtitleEntity");

            migrationBuilder.DropTable(
                name: "TextEntity");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Videos_FileId",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_SubtitleId",
                table: "Videos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Musics_FileId",
                table: "Musics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musics",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_ContentMetaDatas_CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.DropColumn(
                name: "SubtitleId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ContentMetaDatas");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ContentId",
                table: "Videos",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_FileId",
                table: "Videos",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_ContentId",
                table: "Musics",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_FileId",
                table: "Musics",
                column: "FileId",
                unique: true);
        }
    }
}
