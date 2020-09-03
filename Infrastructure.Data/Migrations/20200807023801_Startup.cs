using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioLogin = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Foto = table.Column<string>(maxLength: 250, nullable: false),
                    Biografia = table.Column<string>(maxLength: 1000, nullable: true),
                    TipoHorta = table.Column<string>(maxLength: 50, nullable: true),
                    Interesses = table.Column<string>(maxLength: 50, nullable: true),
                    EstaAtivado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_UsuarioLogin",
                table: "Perfis",
                column: "UsuarioLogin",
                unique: true,
                filter: "[UsuarioLogin] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
