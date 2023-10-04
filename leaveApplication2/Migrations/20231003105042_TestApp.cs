using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class TestApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "employeeId",
                table: "AppliedLeaves",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_employeeId",
                table: "AppliedLeaves",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedLeaves_Employees_employeeId",
                table: "AppliedLeaves",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedLeaves_Employees_employeeId",
                table: "AppliedLeaves");

            migrationBuilder.DropIndex(
                name: "IX_AppliedLeaves_employeeId",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "AppliedLeaves");
        }
    }
}
