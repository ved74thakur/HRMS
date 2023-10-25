using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveStatusId",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "testId",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "leavestatuses",
                columns: table => new
                {
                    LeaveStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeaveStatusName = table.Column<string>(type: "text", nullable: false),
                    LeaveStatusNameCode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leavestatuses", x => x.LeaveStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_LeaveStatusId",
                table: "AppliedLeaves",
                column: "LeaveStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedLeaves_leavestatuses_LeaveStatusId",
                table: "AppliedLeaves",
                column: "LeaveStatusId",
                principalTable: "leavestatuses",
                principalColumn: "LeaveStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaves_leavestatuses_LeaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropTable(
                name: "leavestatuses");

            migrationBuilder.DropIndex(
                name: "IX_AppliedLeaves_LeaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "LeaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "testId",
                table: "AppliedLeaves");
        }
    }
}
