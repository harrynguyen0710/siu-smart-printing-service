using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace siu_smart_printing_service.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertyv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fileSize",
                table: "UploadFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileSize",
                table: "UploadFiles");
        }
    }
}
