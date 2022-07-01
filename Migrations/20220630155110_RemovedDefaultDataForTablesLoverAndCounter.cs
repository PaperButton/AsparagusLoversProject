using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsparagusLoversProject.Migrations
{
    public partial class RemovedDefaultDataForTablesLoverAndCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodIntakeCounters",
                keyColumn: "RecordId",
                keyValue: new Guid("123e4567-e89b-12d3-a456-9ac7cbdcee50"));

            migrationBuilder.DeleteData(
                table: "Lovers",
                keyColumn: "LoverID",
                keyValue: new Guid("123e4567-e89b-12d3-a456-9ac7cbdcee52"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lovers",
                columns: new[] { "LoverID", "EMail", "Fname" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-9ac7cbdcee52"), "ivanSuper@mail.ru", "Ваня" });

            migrationBuilder.InsertData(
                table: "FoodIntakeCounters",
                columns: new[] { "RecordId", "LastFoodEatenDateTime", "LoverID", "NumberOfMealsOfFood" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-9ac7cbdcee50"), new DateTime(2022, 5, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-9ac7cbdcee52"), 1 });
        }
    }
}
