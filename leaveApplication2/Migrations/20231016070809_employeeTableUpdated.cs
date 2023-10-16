using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class employeeTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_LeaveAllocations_leaveAllocationId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ActivationStatuses");

            migrationBuilder.DropTable(
                name: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropIndex(
                name: "IX_Employees_leaveAllocationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "leaveAllocationId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "employeeEmail",
                table: "Employees",
                newName: "emailAddress");

            migrationBuilder.AlterColumn<string>(
                name: "employeePassword",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_emailAddress",
                table: "Employees",
                column: "emailAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_emailAddress",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "emailAddress",
                table: "Employees",
                newName: "employeeEmail");

            migrationBuilder.AlterColumn<string>(
                name: "employeePassword",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "leaveAllocationId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActivationStatuses",
                columns: table => new
                {
                    activationStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activationStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivationStatuses", x => x.activationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                columns: table => new
                {
                    financialYearId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.financialYearId);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocations",
                columns: table => new
                {
                    leaveAllocationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    financialYearId = table.Column<int>(type: "integer", nullable: false),
                    leaveTypeId = table.Column<int>(type: "integer", nullable: false),
                    leaveCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocations", x => x.leaveAllocationId);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_FinancialYears_financialYearId",
                        column: x => x.financialYearId,
                        principalTable: "FinancialYears",
                        principalColumn: "financialYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveAllocations_LeaveTypes_leaveTypeId",
                        column: x => x.leaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "leaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_leaveAllocationId",
                table: "Employees",
                column: "leaveAllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_financialYearId",
                table: "LeaveAllocations",
                column: "financialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_leaveTypeId",
                table: "LeaveAllocations",
                column: "leaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_LeaveAllocations_leaveAllocationId",
                table: "Employees",
                column: "leaveAllocationId",
                principalTable: "LeaveAllocations",
                principalColumn: "leaveAllocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
