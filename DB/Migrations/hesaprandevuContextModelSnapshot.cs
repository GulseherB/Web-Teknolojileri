﻿// <auto-generated />
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(hesaprandevuContext))]
    partial class hesaprandevuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DB.Models.Hesap", b =>
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

            modelBuilder.Entity("DB.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuID"), 1L, 1);

                    b.Property<int>("HesapID")
                        .HasColumnType("int");

                    b.Property<string>("RandevuGun")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RandevuSaat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SacIslemleri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ToplamTutar")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RandevuID");

                    b.HasIndex("HesapID");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("DB.Models.Randevu", b =>
                {
                    b.HasOne("DB.Models.Hesap", "Hesap")
                        .WithMany("Randevular")
                        .HasForeignKey("HesapID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hesap");
                });

            modelBuilder.Entity("DB.Models.Hesap", b =>
                {
                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
