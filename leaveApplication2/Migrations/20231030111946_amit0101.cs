using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class amit0101 : Migration
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
                name: "EmailModels",
                columns: table => new
                {
                    emailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderEmail = table.Column<string>(type: "text", nullable: false),
                    RecipientEmail = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailModels", x => x.emailId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                columns: table => new
                {
                    financialYearId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActiveYear = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.financialYearId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    genderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genderCode = table.Column<string>(type: "text", nullable: false),
                    genderName = table.Column<string>(type: "text", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.genderId);
                });

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
                    emailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    mobileNo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    genderId = table.Column<int>(type: "integer", nullable: false),
                    designationId = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    dateOfJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    employeePassword = table.Column<string>(type: "text", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Employees_Genders_genderId",
                        column: x => x.genderId,
                        principalTable: "Genders",
                        principalColumn: "genderId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "AppliedLeaves",
                columns: table => new
                {
                    appliedLeaveTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<long>(type: "bigint", nullable: false),
                    leaveTypeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LeaveReason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    applyLeaveDay = table.Column<double>(type: "double precision", nullable: false),
                    remaingLeave = table.Column<double>(type: "double precision", nullable: false),
                    balanceLeave = table.Column<double>(type: "double precision", nullable: false),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedNotes = table.Column<string>(type: "text", nullable: true),
                    RejectedNotes = table.Column<string>(type: "text", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RejectedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsHalfDay = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedLeaves", x => x.appliedLeaveTypeId);
                    table.ForeignKey(
                        name: "FK_AppliedLeaves_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
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
                    leaveCount = table.Column<double>(type: "double precision", nullable: false),
                    consumedLeaves = table.Column<double>(type: "double precision", nullable: false),
                    balanceLeaves = table.Column<double>(type: "double precision", nullable: false),
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
                name: "IX_AppliedLeaves_employeeId",
                table: "AppliedLeaves",
                column: "employeeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Employees_emailAddress",
                table: "Employees",
                column: "emailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_genderId",
                table: "Employees",
                column: "genderId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_financialYearId",
                table: "LeaveAllocations",
                column: "financialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_leaveTypeId",
                table: "LeaveAllocations",
                column: "leaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedLeaves");

            migrationBuilder.DropTable(
                name: "EmailModels");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
