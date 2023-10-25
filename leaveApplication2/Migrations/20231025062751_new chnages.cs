using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newchnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMapping_ApplicationPages_ApplicationPageId",
                table: "UserRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMapping_RoleAssign_RoleAssignId",
                table: "UserRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMapping_ApplicationPageId",
                table: "UserRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMapping_RoleAssignId",
                table: "UserRoleMapping");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_ApplicationPageId",
                table: "UserRoleMapping",
                column: "ApplicationPageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_RoleAssignId",
                table: "UserRoleMapping",
                column: "RoleAssignId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMapping_ApplicationPages_ApplicationPageId",
                table: "UserRoleMapping",
                column: "ApplicationPageId",
                principalTable: "ApplicationPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMapping_RoleAssign_RoleAssignId",
                table: "UserRoleMapping",
                column: "RoleAssignId",
                principalTable: "RoleAssign",
                principalColumn: "RoleAssignId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
