using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnEmployeeRegistration22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "employeeId",
                table: "EmployeeRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegistrations_employeeId",
                table: "EmployeeRegistrations",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRegistrations_Employees_employeeId",
                table: "EmployeeRegistrations",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRegistrations_Employees_employeeId",
                table: "EmployeeRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRegistrations_employeeId",
                table: "EmployeeRegistrations");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "EmployeeRegistrations");
        }
    }
}
