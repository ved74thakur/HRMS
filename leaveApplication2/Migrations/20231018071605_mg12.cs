using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class mg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "test",
                table: "FinancialYears");

            migrationBuilder.RenameColumn(
                name: "activeYear",
                table: "FinancialYears",
                newName: "ActiveYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveYear",
                table: "FinancialYears",
                newName: "activeYear");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "LeaveAllocations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "FinancialYears",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
