using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsparagusLoversProject.Migrations
{
    public partial class AddExternalAuthentication4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lovers_AuthenticationProviderrrs_AuthenticationProviderrrID",
                table: "Lovers");

            migrationBuilder.DropIndex(
                name: "IX_Lovers_AuthenticationProviderrrID",
                table: "Lovers");

            migrationBuilder.DropColumn(
                name: "AuthenticationProviderrrID",
                table: "Lovers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthenticationProviderrrID",
                table: "Lovers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lovers_AuthenticationProviderrrID",
                table: "Lovers",
                column: "AuthenticationProviderrrID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lovers_AuthenticationProviderrrs_AuthenticationProviderrrID",
                table: "Lovers",
                column: "AuthenticationProviderrrID",
                principalTable: "AuthenticationProviderrrs",
                principalColumn: "AuthenticationProviderrrID");
        }
    }
}
