using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genero = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Idioma = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nota = table.Column<double>(type: "double", nullable: false),
                    ValorIngresso = table.Column<double>(type: "double", nullable: false),
                    DiaDaExibicao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "double", nullable: false),
                    FilmeId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "DiaDaExibicao", "Genero", "Idioma", "ImagemUrl", "Nome", "Nota", "ValorIngresso" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", "Legendado", "\\efd8fa90-beef-42fd-92a6-7363d6f2a2da.jpg", "Titanic", 9.1999999999999993, 22.5 },
                    { 2, new DateTime(2021, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ficção Científica", "Dublado", "\\6880916d-3518-4b89-a686-fe885653cad8.jpg", "De volta para o futuro", 8.9000000000000004, 20.5 },
                    { 3, new DateTime(2021, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama", "Legendado", "\\762cce23-8936-4022-9cc9-58e53d3d7d39.jpg", "Psicose", 9.5, 22.5 },
                    { 4, new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ficção Científica", "Legendado", "\\1d1087fb-a3e6-4ec4-850a-a01b67043f61.jpg", "Laranja mecânica", 9.0999999999999996, 22.5 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nome", "Role", "Senha" },
                values: new object[,]
                {
                    { 1, "clecio.silva@gft.com", "Clécio", "Administrador", "$DNEFSA$V1$10000$muv8uAcYdk+ST86L5ud+Rcmd5PmDYKTKOc/ADLmOeeuULH+1" },
                    { 2, "stefany.silva@gft.com", "Stefany", "Usuario", "$DNEFSA$V1$10000$OaCvHxEj1uXVB6WcLkl7u3zCByFfAX+IF40x5ieKVFtvSusi" },
                    { 3, "thais.mendes@gft.com", "Thais", "Usuario", "$DNEFSA$V1$10000$IrqIQdghqvbIv2dhM7iyJZWoViN9HH9uTQqfBC40PlPZtiae" }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "FilmeId", "Preco", "Quantidade", "UsuarioId" },
                values: new object[] { 1, 1, 22.5, 1, 2 });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "FilmeId", "Preco", "Quantidade", "UsuarioId" },
                values: new object[] { 2, 2, 20.5, 1, 3 });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "FilmeId", "Preco", "Quantidade", "UsuarioId" },
                values: new object[] { 3, 3, 45.0, 2, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_FilmeId",
                table: "Reservas",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
