using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsparagusLoversProject.Migrations
{
    public partial class AddExternalAuthentication2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthenticationProviderrrs",
                columns: table => new
                {
                    AuthenticationProviderrrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderrrName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationProviderrrs", x => x.AuthenticationProviderrrID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationProviderrrs");
        }
    }
}
