using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaveApplication2.Migrations
{
    /// <inheritdoc />
    public partial class policyDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyDocument",
                table: "PolicyDocument");

            migrationBuilder.RenameTable(
                name: "PolicyDocument",
                newName: "PolicyDocuments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyDocuments",
                table: "PolicyDocuments",
                column: "policyDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyDocuments",
                table: "PolicyDocuments");

            migrationBuilder.RenameTable(
                name: "PolicyDocuments",
                newName: "PolicyDocument");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyDocument",
                table: "PolicyDocument",
                column: "policyDocumentId");
        }
    }
}
