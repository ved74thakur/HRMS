using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnsAddedToEmployeeLeaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "adjustmentAdd",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "adjustmentDel",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "carryForward",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adjustmentAdd",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "adjustmentDel",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "carryForward",
                table: "EmployeeLeaves");
        }
    }
}
