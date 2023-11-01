using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class newColumnActivationStatus23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activationStatus",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "activationStatusId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_activationStatusId",
                table: "Employees",
                column: "activationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ActivationStatuses_activationStatusId",
                table: "Employees",
                column: "activationStatusId",
                principalTable: "ActivationStatuses",
                principalColumn: "activationStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ActivationStatuses_activationStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_activationStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "activationStatusId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "activationStatus",
                table: "Employees",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
