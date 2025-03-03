using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurum.Migrations
{
    /// <inheritdoc />
    public partial class IncomeRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RegularIncomes_AccountId",
                table: "RegularIncomes",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularIncomes_Accounts_AccountId",
                table: "RegularIncomes",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularIncomes_Accounts_AccountId",
                table: "RegularIncomes");

            migrationBuilder.DropIndex(
                name: "IX_RegularIncomes_AccountId",
                table: "RegularIncomes");
        }
    }
}
