using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class employeeUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "leaveAllocationId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_leaveAllocationId",
                table: "Employees",
                column: "leaveAllocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_LeaveAllocations_leaveAllocationId",
                table: "Employees",
                column: "leaveAllocationId",
                principalTable: "LeaveAllocations",
                principalColumn: "leaveAllocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_LeaveAllocations_leaveAllocationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_leaveAllocationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "leaveAllocationId",
                table: "Employees");
        }
    }
}
