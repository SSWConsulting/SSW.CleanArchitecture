using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSW.CleanArchitecture.Infrastructure.Audit.Migrations
{
    /// <inheritdoc />
    public partial class AddChangedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedColumns",
                schema: "audit",
                table: "AuditEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyValues",
                schema: "audit",
                table: "AuditEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                schema: "audit",
                table: "AuditEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldValues",
                schema: "audit",
                table: "AuditEntry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedColumns",
                schema: "audit",
                table: "AuditEntry");

            migrationBuilder.DropColumn(
                name: "KeyValues",
                schema: "audit",
                table: "AuditEntry");

            migrationBuilder.DropColumn(
                name: "NewValues",
                schema: "audit",
                table: "AuditEntry");

            migrationBuilder.DropColumn(
                name: "OldValues",
                schema: "audit",
                table: "AuditEntry");
        }
    }
}
