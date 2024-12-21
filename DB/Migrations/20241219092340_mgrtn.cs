using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    public partial class mgrtn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hesaplar",
                columns: table => new
                {
                    HesapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HesapAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HesapSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HesapEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HesapSifre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hesaplar", x => x.HesapID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuGun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandevuSaat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HesapID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Hesaplar_HesapID",
                        column: x => x.HesapID,
                        principalTable: "Hesaplar",
                        principalColumn: "HesapID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HesapID",
                table: "Randevular",
                column: "HesapID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Hesaplar");
        }
    }
}
