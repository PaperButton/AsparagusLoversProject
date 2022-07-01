using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsparagusLoversProject.Migrations
{
    public partial class initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FoodIntakeCounters_LoverID",
                table: "FoodIntakeCounters",
                column: "LoverID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIntakeCounters_Lovers_LoverID",
                table: "FoodIntakeCounters",
                column: "LoverID",
                principalTable: "Lovers",
                principalColumn: "LoverID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodIntakeCounters_Lovers_LoverID",
                table: "FoodIntakeCounters");

            migrationBuilder.DropIndex(
                name: "IX_FoodIntakeCounters_LoverID",
                table: "FoodIntakeCounters");
        }
    }
}
