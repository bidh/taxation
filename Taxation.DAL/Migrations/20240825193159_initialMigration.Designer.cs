﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taxation.DAL.Context;

#nullable disable

namespace Taxation.DAL.Migrations
{
    [DbContext(typeof(TaxDbContext))]
    [Migration("20240825193159_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Taxation.DAL.Models.DailyTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<float>("Tax")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("DailyTax");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            MunicipalityId = 1,
                            Tax = 0.1f
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTimeOffset(new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            MunicipalityId = 1,
                            Tax = 0.1f
                        });
                });

            modelBuilder.Entity("Taxation.DAL.Models.MonthlyTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("Tax")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("MonthlyTax");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTimeOffset(new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            MunicipalityId = 1,
                            StartDate = new DateTimeOffset(new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Tax = 0.4f
                        });
                });

            modelBuilder.Entity("Taxation.DAL.Models.Municipality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Municipality");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Copenhagen"
                        });
                });

            modelBuilder.Entity("Taxation.DAL.Models.Yearlytax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("Tax")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("Yearlytax");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTimeOffset(new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            MunicipalityId = 1,
                            StartDate = new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Tax = 0.2f
                        });
                });

            modelBuilder.Entity("Taxation.DAL.Models.DailyTax", b =>
                {
                    b.HasOne("Taxation.DAL.Models.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");
                });

            modelBuilder.Entity("Taxation.DAL.Models.MonthlyTax", b =>
                {
                    b.HasOne("Taxation.DAL.Models.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");
                });

            modelBuilder.Entity("Taxation.DAL.Models.Yearlytax", b =>
                {
                    b.HasOne("Taxation.DAL.Models.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");
                });
#pragma warning restore 612, 618
        }
    }
}
