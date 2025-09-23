using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Users_UserId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_UserId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkItems");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_AssignedUserId",
                table: "WorkItems",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Users_AssignedUserId",
                table: "WorkItems",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Users_AssignedUserId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_AssignedUserId",
                table: "WorkItems");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WorkItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_UserId",
                table: "WorkItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Users_UserId",
                table: "WorkItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
