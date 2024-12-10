using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace siu_smart_printing_service.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertyv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    defaultBalance = table.Column<int>(type: "int", nullable: false),
                    maxSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    defaultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");
        }
    }
}
