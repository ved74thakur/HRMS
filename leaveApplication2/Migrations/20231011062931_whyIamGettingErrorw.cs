using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class whyIamGettingErrorw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRegistrations");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "employeePassword",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "leaveCount",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "consumedLeaves",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "balanceLeaves",
                table: "EmployeeLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "remaingLeave",
                table: "AppliedLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "balanceLeave",
                table: "AppliedLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "applyLeaveDay",
                table: "AppliedLeaves",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApprovedNotes",
                table: "AppliedLeaves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "AppliedLeaves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHalfDay",
                table: "AppliedLeaves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "AppliedLeaves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RejectedNotes",
                table: "AppliedLeaves",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeePassword",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "ApprovedNotes",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "IsHalfDay",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "AppliedLeaves");

            migrationBuilder.DropColumn(
                name: "RejectedNotes",
                table: "AppliedLeaves");

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Employees",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Employees",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<int>(
                name: "leaveCount",
                table: "EmployeeLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "consumedLeaves",
                table: "EmployeeLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "balanceLeaves",
                table: "EmployeeLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "remaingLeave",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "balanceLeave",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "applyLeaveDay",
                table: "AppliedLeaves",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "EmployeeRegistrations",
                columns: table => new
                {
                    employeeRegistrationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    employeeName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    passwordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRegistrations", x => x.employeeRegistrationId);
                });
        }
    }
}
