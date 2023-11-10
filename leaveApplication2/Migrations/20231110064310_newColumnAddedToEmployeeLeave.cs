using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnAddedToEmployeeLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "leaveAllocationId",
                table: "EmployeeLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_leaveAllocationId",
                table: "EmployeeLeaves",
                column: "leaveAllocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_LeaveAllocations_leaveAllocationId",
                table: "EmployeeLeaves",
                column: "leaveAllocationId",
                principalTable: "LeaveAllocations",
                principalColumn: "leaveAllocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_LeaveAllocations_leaveAllocationId",
                table: "EmployeeLeaves");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeLeaves_leaveAllocationId",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "leaveAllocationId",
                table: "EmployeeLeaves");
        }
    }
}
