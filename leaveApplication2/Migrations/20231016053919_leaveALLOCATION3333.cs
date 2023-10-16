using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class leaveALLOCATION3333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_Employees_employeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_employeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "LeaveAllocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "employeeId",
                table: "LeaveAllocations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_employeeId",
                table: "LeaveAllocations",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_Employees_employeeId",
                table: "LeaveAllocations",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
