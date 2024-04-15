using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDddBoundaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Teams_TeamId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_TeamId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Heroes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Heroes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_TeamId",
                table: "Heroes",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Teams_TeamId",
                table: "Heroes",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
