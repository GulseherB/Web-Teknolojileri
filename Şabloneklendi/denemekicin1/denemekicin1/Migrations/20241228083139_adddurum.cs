using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace denemekicin1.Migrations
{
    public partial class adddurum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Durum",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Randevular");
        }
    }
}
