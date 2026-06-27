using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CHARIBOY_ARTS.Migrations
{
    /// <inheritdoc />
    public partial class newVideoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "VideoData",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Videos",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "Description", "FileName", "FilePath", "Title" },
                values: new object[] { 2, "Love", "", "", "Tiger" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Videos",
                newName: "ContentType");

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoData",
                table: "Videos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ContentType", "FileName", "VideoData" },
                values: new object[] { 1, "", "", new byte[0] });
        }
    }
}
