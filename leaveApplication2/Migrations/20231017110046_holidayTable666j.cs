using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class holidayTable666j : Migration
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

            migrationBuilder.DropColumn(
                name: "leaveStatusId",
                table: "AppliedLeaves");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveYear",
                table: "FinancialYears",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HolidayName = table.Column<string>(type: "text", nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropColumn(
                name: "ActiveYear",
                table: "FinancialYears");

            migrationBuilder.AddColumn<int>(
                name: "leaveStatusId",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
