﻿// <auto-generated />
using GeekShopping.CuponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CuponAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20220607144157_SeedCuponDataBase")]
    partial class SeedCuponDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.CuponAPI.Model.Cupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("CuponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("cupon_code");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount-amount");

                    b.HasKey("Id");

                    b.ToTable("cupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CuponCode = "ERUDIO_2022_10",
                            DiscountAmount = 10m
                        },
                        new
                        {
                            Id = 2L,
                            CuponCode = "ERUDIO_2022_15",
                            DiscountAmount = 15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}