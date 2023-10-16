using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class updatedEmployeeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ActivationStatuses_activationStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "activationStatusId",
                table: "Employees",
                newName: "genderId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_activationStatusId",
                table: "Employees",
                newName: "IX_Employees_genderId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "dateOfBirth",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "dateOfJoining",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "mobileNo",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AppliedLeaves",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RejectedDate",
                table: "AppliedLeaves",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AppliedLeaves",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "AppliedLeaves",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Genders_genderId",
                table: "Employees",
                column: "genderId",
                principalTable: "Genders",
                principalColumn: "genderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Genders_genderId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropColumn(
                name: "dateOfBirth",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "dateOfJoining",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "mobileNo",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "genderId",
                table: "Employees",
                newName: "activationStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_genderId",
                table: "Employees",
                newName: "IX_Employees_activationStatusId");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RejectedDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedDate",
                table: "AppliedLeaves",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ActivationStatuses_activationStatusId",
                table: "Employees",
                column: "activationStatusId",
                principalTable: "ActivationStatuses",
                principalColumn: "activationStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
