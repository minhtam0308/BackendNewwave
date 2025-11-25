using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateurlImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBook",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "UrlBook",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlBook",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBook",
                table: "Books",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
