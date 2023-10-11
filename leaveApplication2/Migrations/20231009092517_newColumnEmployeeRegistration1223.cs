using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnEmployeeRegistration1223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "employeeEmail",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeeEmail",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Employees");

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
    }
}
