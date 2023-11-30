using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class addedConstraintToFinancialYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_endDate",
                table: "FinancialYears",
                column: "endDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_startDate",
                table: "FinancialYears",
                column: "startDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialYears_endDate",
                table: "FinancialYears");

            migrationBuilder.DropIndex(
                name: "IX_FinancialYears_startDate",
                table: "FinancialYears");
        }
    }
}
