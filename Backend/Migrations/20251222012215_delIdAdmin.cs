using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class delIdAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Users_IdAdmin",
                table: "Borrows");

            migrationBuilder.DropIndex(
                name: "IX_Borrows_IdAdmin",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "IdAdmin",
                table: "Borrows");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Borrows",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_UserId",
                table: "Borrows",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Users_UserId",
                table: "Borrows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Users_UserId",
                table: "Borrows");

            migrationBuilder.DropIndex(
                name: "IX_Borrows_UserId",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Borrows");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAdmin",
                table: "Borrows",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_IdAdmin",
                table: "Borrows",
                column: "IdAdmin");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Users_IdAdmin",
                table: "Borrows",
                column: "IdAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
