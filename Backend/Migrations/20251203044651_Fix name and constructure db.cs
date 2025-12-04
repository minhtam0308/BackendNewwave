using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Fixnameandconstructuredb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_IdAuthor",
                table: "Books");

            migrationBuilder.DropTable(
                name: "ChiTietMuons");

            migrationBuilder.DropTable(
                name: "MuonTras");

            migrationBuilder.CreateTable(
                name: "Borrows",
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

            migrationBuilder.CreateTable(
                name: "DetailBorrows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBorrow = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBook = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailBorrows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailBorrows_Borrows_IdBorrow",
                        column: x => x.IdBorrow,
                        principalTable: "Borrows",
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

            migrationBuilder.CreateIndex(
                name: "IX_DetailBorrows_IdBorrow",
                table: "DetailBorrows",
                column: "IdBorrow");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_IdAuthor",
                table: "Books",
                column: "IdAuthor",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_IdAuthor",
                table: "Books");

            migrationBuilder.DropTable(
                name: "DetailBorrows");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.CreateTable(
                name: "MuonTras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    hanDenMuon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hanDenTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayMuon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayTra = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuonTras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuonTras_Users_IdAdmin",
                        column: x => x.IdAdmin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MuonTras_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietMuons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMuonTra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBook = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietMuons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietMuons_MuonTras_IdMuonTra",
                        column: x => x.IdMuonTra,
                        principalTable: "MuonTras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietMuons_IdMuonTra",
                table: "ChiTietMuons",
                column: "IdMuonTra");

            migrationBuilder.CreateIndex(
                name: "IX_MuonTras_IdAdmin",
                table: "MuonTras",
                column: "IdAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_MuonTras_IdUser",
                table: "MuonTras",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_IdAuthor",
                table: "Books",
                column: "IdAuthor",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
