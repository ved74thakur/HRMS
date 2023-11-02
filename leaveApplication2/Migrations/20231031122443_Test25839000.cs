using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Test25839000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "componentName",
                table: "ApplicationPage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isMenuPage",
                table: "ApplicationPage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "menuPath",
                table: "ApplicationPage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "routePath",
                table: "ApplicationPage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_ApplicationPageId",
                table: "UserRoleMapping",
                column: "ApplicationPageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_RoleAssignId",
                table: "UserRoleMapping",
                column: "RoleAssignId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMapping_ApplicationPage_ApplicationPageId",
                table: "UserRoleMapping",
                column: "ApplicationPageId",
                principalTable: "ApplicationPage",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMapping_ApplicationPage_ApplicationPageId",
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

            migrationBuilder.DropColumn(
                name: "componentName",
                table: "ApplicationPage");

            migrationBuilder.DropColumn(
                name: "isMenuPage",
                table: "ApplicationPage");

            migrationBuilder.DropColumn(
                name: "menuPath",
                table: "ApplicationPage");

            migrationBuilder.DropColumn(
                name: "routePath",
                table: "ApplicationPage");
        }
    }
}
