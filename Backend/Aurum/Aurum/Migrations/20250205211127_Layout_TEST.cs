using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurum.Migrations
{
    /// <inheritdoc />
    public partial class Layout_TEST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicLayouts",
                columns: table => new
                {
                    BasicLayoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Chart1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart7 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicLayouts", x => x.BasicLayoutId);
                });

            migrationBuilder.CreateTable(
                name: "DetailedLayouts",
                columns: table => new
                {
                    DetailedLayoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Chart1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart9 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedLayouts", x => x.DetailedLayoutId);
                });

            migrationBuilder.CreateTable(
                name: "ScienticLayouts",
                columns: table => new
                {
                    ScienticLayoutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Chart1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chart8 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienticLayouts", x => x.ScienticLayoutId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicLayouts");

            migrationBuilder.DropTable(
                name: "DetailedLayouts");

            migrationBuilder.DropTable(
                name: "ScienticLayouts");
        }
    }
}
