using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace denemekicin1.Migrations
{
    public partial class columnalan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alan",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alan",
                table: "Calisanlar");
        }
    }
}
