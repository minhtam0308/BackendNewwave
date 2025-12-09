using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class editDetailborrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DetailBorrows_IdBook",
                table: "DetailBorrows",
                column: "IdBook");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBorrows_Books_IdBook",
                table: "DetailBorrows",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBorrows_Books_IdBook",
                table: "DetailBorrows");

            migrationBuilder.DropIndex(
                name: "IX_DetailBorrows_IdBook",
                table: "DetailBorrows");
        }
    }
}
