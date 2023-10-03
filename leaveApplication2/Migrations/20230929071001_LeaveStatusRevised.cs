using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class LeaveStatusRevised : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "leaveStatusId",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaves_LeaveStatuses_leaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropIndex(
                name: "IX_AppliedLeaves_leaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "leaveStatusId",
                table: "AppliedLeaves");
        }
    }
}
