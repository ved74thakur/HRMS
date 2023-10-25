using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationPageId = table.Column<int>(type: "integer", nullable: false),
                    RoleAssignId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_ApplicationPages_ApplicationPageId",
                        column: x => x.ApplicationPageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_RoleAssign_RoleAssignId",
                        column: x => x.RoleAssignId,
                        principalTable: "RoleAssign",
                        principalColumn: "RoleAssignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_ApplicationPageId",
                table: "UserRoleMapping",
                column: "ApplicationPageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_RoleAssignId",
                table: "UserRoleMapping",
                column: "RoleAssignId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleMapping");
        }
    }
}
