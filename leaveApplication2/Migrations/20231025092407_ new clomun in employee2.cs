using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newclomuninemployee2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_roleID",
                table: "Employees",
                column: "roleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_RoleAssign_roleID",
                table: "Employees",
                column: "roleID",
                principalTable: "RoleAssign",
                principalColumn: "RoleAssignId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_RoleAssign_roleID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_roleID",
                table: "Employees");
        }
    }
}
