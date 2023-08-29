using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hjc.TranslatorDomain.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnglishWords",
                columns: table => new
                {
                    EnglishId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Explain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishWords", x => x.EnglishId);
                });

            migrationBuilder.CreateTable(
                name: "ChineseWords",
                columns: table => new
                {
                    ChineseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EnglishWordEnglishId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChineseWords", x => x.ChineseId);
                    table.ForeignKey(
                        name: "FK_ChineseWords_EnglishWords_EnglishWordEnglishId",
                        column: x => x.EnglishWordEnglishId,
                        principalTable: "EnglishWords",
                        principalColumn: "EnglishId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChineseWords_EnglishWordEnglishId",
                table: "ChineseWords",
                column: "EnglishWordEnglishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChineseWords");

            migrationBuilder.DropTable(
                name: "EnglishWords");
        }
    }
}
