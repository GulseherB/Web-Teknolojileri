using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace denemekicin1.Migrations
{
    public partial class mgrtn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CalisanSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanID);
                });

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
                    RandevuSaat = table.Column<int>(type: "int", nullable: false),
                    RandevuBitisSaati = table.Column<int>(type: "int", nullable: false),
                    HesapID = table.Column<int>(type: "int", nullable: false),
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    SacIslemleri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemSuresi = table.Column<int>(type: "int", nullable: false),
                    ToplamTutar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Hesaplar_HesapID",
                        column: x => x.HesapID,
                        principalTable: "Hesaplar",
                        principalColumn: "HesapID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanID",
                table: "Randevular",
                column: "CalisanID");

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
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Hesaplar");
        }
    }
}
