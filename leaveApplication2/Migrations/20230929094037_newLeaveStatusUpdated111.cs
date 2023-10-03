using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newLeaveStatusUpdated111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaves_LeaveStatuses_leaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropTable(
                name: "LeaveStatuses");

            migrationBuilder.DropIndex(
                name: "IX_AppliedLeaves_leaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.RenameColumn(
                name: "numberOfDays",
                table: "AppliedLeaves",
                newName: "remaingLeave");

            migrationBuilder.RenameColumn(
                name: "leaveStatusId",
                table: "AppliedLeaves",
                newName: "balanceLeave");

            migrationBuilder.AddColumn<int>(
                name: "applyLeave",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applyLeave",
                table: "AppliedLeaves");

            migrationBuilder.RenameColumn(
                name: "remaingLeave",
                table: "AppliedLeaves",
                newName: "numberOfDays");

            migrationBuilder.RenameColumn(
                name: "balanceLeave",
                table: "AppliedLeaves",
                newName: "leaveStatusId");

            migrationBuilder.CreateTable(
                name: "LeaveStatuses",
                columns: table => new
                {
                    leaveStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    leaveStatusName = table.Column<string>(type: "text", nullable: false),
                    leaveStatusNameCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveStatuses", x => x.leaveStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_leaveStatusId",
                table: "AppliedLeaves",
                column: "leaveStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedLeaves_LeaveStatuses_leaveStatusId",
                table: "AppliedLeaves",
                column: "leaveStatusId",
                principalTable: "LeaveStatuses",
                principalColumn: "leaveStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
