using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaveComments_Employees_employeeId",
                table: "AppliedLeaveComments");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "AppliedLeaveComments",
                newName: "createdEmpId");

            migrationBuilder.RenameIndex(
                name: "IX_AppliedLeaveComments_employeeId",
                table: "AppliedLeaveComments",
                newName: "IX_AppliedLeaveComments_createdEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedLeaveComments_Employees_createdEmpId",
                table: "AppliedLeaveComments",
                column: "createdEmpId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaveComments_Employees_createdEmpId",
                table: "AppliedLeaveComments");

            migrationBuilder.RenameColumn(
                name: "createdEmpId",
                table: "AppliedLeaveComments",
                newName: "employeeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppliedLeaveComments_createdEmpId",
                table: "AppliedLeaveComments",
                newName: "IX_AppliedLeaveComments_employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedLeaveComments_Employees_employeeId",
                table: "AppliedLeaveComments",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
