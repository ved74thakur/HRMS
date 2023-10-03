using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class LeaveStatus1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statusName",
                table: "LeaveStatuses",
                newName: "leaveStatusNameCode");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "LeaveStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "leaveStatusName",
                table: "LeaveStatuses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "LeaveStatuses");

            migrationBuilder.DropColumn(
                name: "leaveStatusName",
                table: "LeaveStatuses");

            migrationBuilder.RenameColumn(
                name: "leaveStatusNameCode",
                table: "LeaveStatuses",
                newName: "statusName");
        }
    }
}
