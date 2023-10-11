using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "foto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titolo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Visibilità = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaFoto",
                columns: table => new
                {
                    CategorieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaFoto", x => new { x.CategorieId, x.FotosId });
                    table.ForeignKey(
                        name: "FK_CategoriaFoto_Categorie_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaFoto_foto_FotosId",
                        column: x => x.FotosId,
                        principalTable: "foto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaFoto_FotosId",
                table: "CategoriaFoto",
                column: "FotosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaFoto");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "foto");
        }
    }
}
