using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateEnglishDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBorrows_Borrows_IdBorrow",
                table: "DetailBorrows");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.RenameColumn(
                name: "sdt",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Lop",
                table: "Users",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "Khoa",
                table: "Users",
                newName: "Class");

            migrationBuilder.CreateTable(
                name: "Boro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresBorrow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealTimeBorrow = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealTimeReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boro_Users_IdAdmin",
                        column: x => x.IdAdmin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boro_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boro_IdAdmin",
                table: "Boro",
                column: "IdAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Boro_IdUser",
                table: "Boro",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBorrows_Boro_IdBorrow",
                table: "DetailBorrows",
                column: "IdBorrow",
                principalTable: "Boro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBorrows_Boro_IdBorrow",
                table: "DetailBorrows");

            migrationBuilder.DropTable(
                name: "Boro");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "sdt");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Users",
                newName: "Lop");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Users",
                newName: "Khoa");

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresBorrow = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RealTimeBorrow = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealTimeReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borrows_Users_IdAdmin",
                        column: x => x.IdAdmin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrows_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_IdAdmin",
                table: "Borrows",
                column: "IdAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_IdUser",
                table: "Borrows",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBorrows_Borrows_IdBorrow",
                table: "DetailBorrows",
                column: "IdBorrow",
                principalTable: "Borrows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
