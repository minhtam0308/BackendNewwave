using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateBook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartBooks_Carts_IdBook",
                table: "CartBooks");

            migrationBuilder.RenameColumn(
                name: "IdCard",
                table: "CartBooks",
                newName: "IdCart");

            migrationBuilder.CreateIndex(
                name: "IX_CartBooks_IdCart",
                table: "CartBooks",
                column: "IdCart");

            migrationBuilder.AddForeignKey(
                name: "FK_CartBooks_Books_IdBook",
                table: "CartBooks",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartBooks_Carts_IdCart",
                table: "CartBooks",
                column: "IdCart",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartBooks_Books_IdBook",
                table: "CartBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_CartBooks_Carts_IdCart",
                table: "CartBooks");

            migrationBuilder.DropIndex(
                name: "IX_CartBooks_IdCart",
                table: "CartBooks");

            migrationBuilder.RenameColumn(
                name: "IdCart",
                table: "CartBooks",
                newName: "IdCard");

            migrationBuilder.AddForeignKey(
                name: "FK_CartBooks_Carts_IdBook",
                table: "CartBooks",
                column: "IdBook",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
