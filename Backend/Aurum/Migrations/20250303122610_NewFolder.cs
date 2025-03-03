using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aurum.Migrations
{
    /// <inheritdoc />
    public partial class NewFolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CurrencyCode", "Name", "Symbol" },
                values: new object[,]
                {
                    { -4, "GBP", "Font", "P" },
                    { -3, "USD", "US Dollar", "$" },
                    { -2, "EUR", "Euro", "€" },
                    { -1, "HUF", "Forint", "Ft" }
                });
        }
    }
}
