using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    designationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    designationName = table.Column<string>(type: "text", nullable: false),
                    designationCode = table.Column<string>(type: "text", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.designationId);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    leaveTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    leaveTypeName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    leaveTypeNameCode = table.Column<string>(type: "text", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.leaveTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeCode = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.employeeId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeCode = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    designationId = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_designationId",
                        column: x => x.designationId,
                        principalTable: "Designations",
                        principalColumn: "designationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedLeaves",
                columns: table => new
                {
                    appliedLeaveTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    leaveTypeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaveReason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedLeaves", x => x.appliedLeaveTypeId);
                    table.ForeignKey(
                        name: "FK_AppliedLeaves_LeaveTypes_leaveTypeId",
                        column: x => x.leaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "leaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaves",
                columns: table => new
                {
                    employeeLeaveId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
                    leaveTypeId = table.Column<int>(type: "integer", nullable: false),
                    leaveCount = table.Column<int>(type: "integer", nullable: false),
                    consumedLeaves = table.Column<int>(type: "integer", nullable: false),
                    balanceLeaves = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.employeeLeaveId);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_LeaveTypes_leaveTypeId",
                        column: x => x.leaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "leaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_leaveTypeId",
                table: "AppliedLeaves",
                column: "leaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_employeeId",
                table: "EmployeeLeaves",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_leaveTypeId",
                table: "EmployeeLeaves",
                column: "leaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_designationId",
                table: "Employees",
                column: "designationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedLeaves");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Designations");
        }
    }
}
