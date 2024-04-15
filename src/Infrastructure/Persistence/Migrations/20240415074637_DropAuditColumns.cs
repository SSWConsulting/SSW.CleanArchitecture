using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropAuditColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Heroes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TodoItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "TodoItems",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Teams",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Teams",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Mission",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Mission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Mission",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Mission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Heroes",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Heroes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
