using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taxation.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTax_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyTax_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yearlytax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Tax = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yearlytax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yearlytax_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Municipality",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Copenhagen" });

            migrationBuilder.InsertData(
                table: "DailyTax",
                columns: new[] { "Id", "Date", "MunicipalityId", "Tax" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0.1f },
                    { 2, new DateTimeOffset(new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 0.1f }
                });

            migrationBuilder.InsertData(
                table: "MonthlyTax",
                columns: new[] { "Id", "EndDate", "MunicipalityId", "StartDate", "Tax" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), 0.4f });

            migrationBuilder.InsertData(
                table: "Yearlytax",
                columns: new[] { "Id", "EndDate", "MunicipalityId", "StartDate", "Tax" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 0.2f });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTax_MunicipalityId",
                table: "DailyTax",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyTax_MunicipalityId",
                table: "MonthlyTax",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Yearlytax_MunicipalityId",
                table: "Yearlytax",
                column: "MunicipalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTax");

            migrationBuilder.DropTable(
                name: "MonthlyTax");

            migrationBuilder.DropTable(
                name: "Yearlytax");

            migrationBuilder.DropTable(
                name: "Municipality");
        }
    }
}
