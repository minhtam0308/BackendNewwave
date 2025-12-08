using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateforenkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "CartBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartBooks_BookId",
                table: "CartBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartBooks_Books_BookId",
                table: "CartBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartBooks_Books_BookId",
                table: "CartBooks");

            migrationBuilder.DropIndex(
                name: "IX_CartBooks_BookId",
                table: "CartBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CartBooks");
        }
    }
}
