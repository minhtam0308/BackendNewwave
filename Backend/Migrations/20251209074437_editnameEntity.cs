using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class editnameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartBooks_Books_BookId",
                table: "CartBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartBooks_BookId",
                table: "CartBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CartBooks");

            migrationBuilder.RenameColumn(
                name: "urlUserImage",
                table: "Users",
                newName: "UrlUserImage");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Carts",
                newName: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_IdUser",
                table: "Carts",
                column: "IdUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_IdUser",
                table: "Carts",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_IdUser",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_IdUser",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "UrlUserImage",
                table: "Users",
                newName: "urlUserImage");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Carts",
                newName: "idUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "CartBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
