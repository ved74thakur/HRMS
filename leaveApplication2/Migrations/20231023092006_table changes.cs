using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class tablechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoleAssign",
                newName: "RoleAssignName");

            migrationBuilder.RenameColumn(
                name: "CodeName",
                table: "RoleAssign",
                newName: "RoleAssignCodeName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoleAssign",
                newName: "RoleAssignId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleAssignName",
                table: "RoleAssign",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RoleAssignCodeName",
                table: "RoleAssign",
                newName: "CodeName");

            migrationBuilder.RenameColumn(
                name: "RoleAssignId",
                table: "RoleAssign",
                newName: "Id");
        }
    }
}
