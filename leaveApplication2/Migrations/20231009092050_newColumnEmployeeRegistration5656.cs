using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnEmployeeRegistration5656 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "employeeRegistrationId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employeeRegistrationId",
                table: "Employees",
                column: "employeeRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRegistrations_employeeRegistrationId",
                table: "Employees",
                column: "employeeRegistrationId",
                principalTable: "EmployeeRegistrations",
                principalColumn: "employeeRegistrationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRegistrations_employeeRegistrationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_employeeRegistrationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "employeeRegistrationId",
                table: "Employees");
        }
    }
}
