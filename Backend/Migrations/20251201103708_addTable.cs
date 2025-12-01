using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class addTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Khoa",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lop",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sdt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MuonTras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hanDenMuon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayMuon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    hanDenTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    IdBook = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietMuons");

            migrationBuilder.DropTable(
                name: "MuonTras");

            migrationBuilder.DropColumn(
                name: "Khoa",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lop",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "sdt",
                table: "Users");
        }
    }
}
