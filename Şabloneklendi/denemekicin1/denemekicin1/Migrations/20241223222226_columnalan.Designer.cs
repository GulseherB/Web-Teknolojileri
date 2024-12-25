﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using denemekicin1.Models;

#nullable disable

namespace denemekicin1.Migrations
{
    [DbContext(typeof(hesaprandevuContext))]
    [Migration("20241223222226_columnalan")]
    partial class columnalan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("denemekicin1.Models.Calisan", b =>
                {
                    b.Property<int>("CalisanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanID"), 1L, 1);

                    b.Property<string>("CalisanAd")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CalisanSoyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CalisanID");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("denemekicin1.Models.Hesap", b =>
                {
                    b.Property<int>("HesapID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HesapID"), 1L, 1);

                    b.Property<string>("HesapAd")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HesapEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HesapSifre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HesapSoyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("HesapID");

                    b.ToTable("Hesaplar");
                });

            modelBuilder.Entity("denemekicin1.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuID"), 1L, 1);

                    b.Property<int>("CalisanID")
                        .HasColumnType("int");

                    b.Property<int>("HesapID")
                        .HasColumnType("int");

                    b.Property<int>("IslemSuresi")
                        .HasColumnType("int");

                    b.Property<int>("RandevuBitisSaati")
                        .HasColumnType("int");

                    b.Property<string>("RandevuGun")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RandevuSaat")
                        .HasColumnType("int");

                    b.Property<string>("SacIslemleri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToplamTutar")
                        .HasColumnType("int");

                    b.HasKey("RandevuID");

                    b.HasIndex("CalisanID");

                    b.HasIndex("HesapID");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("denemekicin1.Models.Randevu", b =>
                {
                    b.HasOne("denemekicin1.Models.Calisan", "Calisan")
                        .WithMany("Randevular")
                        .HasForeignKey("CalisanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("denemekicin1.Models.Hesap", "Hesap")
                        .WithMany("Randevular")
                        .HasForeignKey("HesapID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Hesap");
                });

            modelBuilder.Entity("denemekicin1.Models.Calisan", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("denemekicin1.Models.Hesap", b =>
                {
                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
