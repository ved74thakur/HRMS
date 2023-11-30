using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class phase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageName = table.Column<string>(type: "text", nullable: false),
                    PageCode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    routePath = table.Column<string>(type: "text", nullable: false),
                    menuPath = table.Column<string>(type: "text", nullable: false),
                    isMenuPage = table.Column<bool>(type: "boolean", nullable: false),
                    componentName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPage", x => x.Id);
                });

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
                name: "EmployeeReporting",
                columns: table => new
                {
                    EmployeeReportingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ReportingPersonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeReporting", x => x.EmployeeReportingId);
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
                name: "LeaveStatuses",
                columns: table => new
                {
                    LeaveStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeaveStatusName = table.Column<string>(type: "text", nullable: false),
                    LeaveStatusCode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveStatuses", x => x.LeaveStatusId);
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
                name: "PolicyDocuments",
                columns: table => new
                {
                    policyDocumentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    policyName = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<byte[]>(type: "bytea", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyDocuments", x => x.policyDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "RoleAssign",
                columns: table => new
                {
                    RoleAssignId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleAssignName = table.Column<string>(type: "text", nullable: false),
                    RoleAssignCodeName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAssign", x => x.RoleAssignId);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeCode = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    employeeName = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.employeeId);
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
                    employeePassword = table.Column<string>(type: "text", nullable: false),
                    RoleAssignId = table.Column<int>(type: "integer", nullable: false),
                    ReportingPersonId = table.Column<long>(type: "bigint", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Employees_RoleAssign_RoleAssignId",
                        column: x => x.RoleAssignId,
                        principalTable: "RoleAssign",
                        principalColumn: "RoleAssignId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_UserRoleMapping_ApplicationPage_ApplicationPageId",
                        column: x => x.ApplicationPageId,
                        principalTable: "ApplicationPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_RoleAssign_RoleAssignId",
                        column: x => x.RoleAssignId,
                        principalTable: "RoleAssign",
                        principalColumn: "RoleAssignId",
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
                    LeaveStatusId = table.Column<int>(type: "integer", nullable: false),
                    remaingLeave = table.Column<double>(type: "double precision", nullable: false),
                    balanceLeave = table.Column<double>(type: "double precision", nullable: false),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
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
                        name: "FK_AppliedLeaves_LeaveStatuses_LeaveStatusId",
                        column: x => x.LeaveStatusId,
                        principalTable: "LeaveStatuses",
                        principalColumn: "LeaveStatusId",
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
                    leaveAllocationId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_EmployeeLeaves_LeaveAllocations_leaveAllocationId",
                        column: x => x.leaveAllocationId,
                        principalTable: "LeaveAllocations",
                        principalColumn: "leaveAllocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_LeaveTypes_leaveTypeId",
                        column: x => x.leaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "leaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedLeaveComments",
                columns: table => new
                {
                    appliedLeaveCommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    appliedLeaveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    LeaveStatusId = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedLeaveComments", x => x.appliedLeaveCommentId);
                    table.ForeignKey(
                        name: "FK_AppliedLeaveComments_AppliedLeaves_appliedLeaveTypeId",
                        column: x => x.appliedLeaveTypeId,
                        principalTable: "AppliedLeaves",
                        principalColumn: "appliedLeaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedLeaveComments_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "employeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedLeaveComments_LeaveStatuses_LeaveStatusId",
                        column: x => x.LeaveStatusId,
                        principalTable: "LeaveStatuses",
                        principalColumn: "LeaveStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaveComments_appliedLeaveTypeId",
                table: "AppliedLeaveComments",
                column: "appliedLeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaveComments_employeeId",
                table: "AppliedLeaveComments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaveComments_LeaveStatusId",
                table: "AppliedLeaveComments",
                column: "LeaveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_employeeId",
                table: "AppliedLeaves",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_LeaveStatusId",
                table: "AppliedLeaves",
                column: "LeaveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLeaves_leaveTypeId",
                table: "AppliedLeaves",
                column: "leaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_employeeId",
                table: "EmployeeLeaves",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_leaveAllocationId",
                table: "EmployeeLeaves",
                column: "leaveAllocationId");

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
                name: "IX_Employees_RoleAssignId",
                table: "Employees",
                column: "RoleAssignId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_endDate",
                table: "FinancialYears",
                column: "endDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYears_startDate",
                table: "FinancialYears",
                column: "startDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_financialYearId",
                table: "LeaveAllocations",
                column: "financialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_leaveTypeId",
                table: "LeaveAllocations",
                column: "leaveTypeId");

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
                name: "AppliedLeaveComments");

            migrationBuilder.DropTable(
                name: "EmailModels");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "EmployeeReporting");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "PolicyDocuments");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "UserRoleMapping");

            migrationBuilder.DropTable(
                name: "AppliedLeaves");

            migrationBuilder.DropTable(
                name: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "ApplicationPage");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveStatuses");

            migrationBuilder.DropTable(
                name: "FinancialYears");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "RoleAssign");
        }
    }
}
