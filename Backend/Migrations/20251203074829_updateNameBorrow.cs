using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateNameBorrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boro_Users_IdAdmin",
                table: "Boro");

            migrationBuilder.DropForeignKey(
                name: "FK_Boro_Users_IdUser",
                table: "Boro");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailBorrows_Boro_IdBorrow",
                table: "DetailBorrows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boro",
                table: "Boro");

            migrationBuilder.RenameTable(
                name: "Boro",
                newName: "Borrows");

            migrationBuilder.RenameIndex(
                name: "IX_Boro_IdUser",
                table: "Borrows",
                newName: "IX_Borrows_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Boro_IdAdmin",
                table: "Borrows",
                newName: "IX_Borrows_IdAdmin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Borrows",
                table: "Borrows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Users_IdAdmin",
                table: "Borrows",
                column: "IdAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Users_IdUser",
                table: "Borrows",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBorrows_Borrows_IdBorrow",
                table: "DetailBorrows",
                column: "IdBorrow",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Users_IdAdmin",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Users_IdUser",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailBorrows_Borrows_IdBorrow",
                table: "DetailBorrows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Borrows",
                table: "Borrows");

            migrationBuilder.RenameTable(
                name: "Borrows",
                newName: "Boro");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_IdUser",
                table: "Boro",
                newName: "IX_Boro_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Borrows_IdAdmin",
                table: "Boro",
                newName: "IX_Boro_IdAdmin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boro",
                table: "Boro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boro_Users_IdAdmin",
                table: "Boro",
                column: "IdAdmin",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boro_Users_IdUser",
                table: "Boro",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBorrows_Boro_IdBorrow",
                table: "DetailBorrows",
                column: "IdBorrow",
                principalTable: "Boro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
