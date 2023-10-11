using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnEmployeeRegistration733 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "EmployeeRegistrations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "EmployeeRegistrations");

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
    }
}
