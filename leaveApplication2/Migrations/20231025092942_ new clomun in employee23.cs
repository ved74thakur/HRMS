using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newclomuninemployee23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_RoleAssign_roleID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "roleID",
                table: "Employees",
                newName: "RoleAssignId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_roleID",
                table: "Employees",
                newName: "IX_Employees_RoleAssignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_RoleAssign_RoleAssignId",
                table: "Employees",
                column: "RoleAssignId",
                principalTable: "RoleAssign",
                principalColumn: "RoleAssignId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_RoleAssign_RoleAssignId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RoleAssignId",
                table: "Employees",
                newName: "roleID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RoleAssignId",
                table: "Employees",
                newName: "IX_Employees_roleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_RoleAssign_roleID",
                table: "Employees",
                column: "roleID",
                principalTable: "RoleAssign",
                principalColumn: "RoleAssignId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
