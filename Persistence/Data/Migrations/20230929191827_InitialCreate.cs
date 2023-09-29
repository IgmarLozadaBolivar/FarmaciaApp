using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombre del genero")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Abreviatura = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "Abreviatura del genero")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Descripcion del genero")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombre del pais")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capital = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Capital del pais")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoISO = table.Column<string>(name: "Codigo ISO", type: "varchar(3)", maxLength: 3, nullable: false, comment: "Abreviatura del pais")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Moneda = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Moneda del pais")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Idioma = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Idioma del pais")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tipo Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombre del tipo de persona")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Descripcion del tipo de persona")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo Persona", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombre del departamento")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoISO = table.Column<string>(name: "Codigo ISO", type: "varchar(3)", maxLength: 3, nullable: false, comment: "Abreviatura del departamento")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPaisFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Pais_IdPaisFK",
                        column: x => x.IdPaisFK,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombre de la ciudad")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdDepFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Departamento_IdDepFK",
                        column: x => x.IdDepFK,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nombres de la persona")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellidos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Apellidos de la persona")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Edad = table.Column<int>(type: "int", maxLength: 3, nullable: false, comment: "Edad de la persona"),
                    IdCiuFK = table.Column<int>(type: "int", nullable: false),
                    FechadeNacimiento = table.Column<DateOnly>(name: "Fecha de Nacimiento", type: "date", nullable: false, comment: "Fecha de nacimiento de la persona"),
                    IdGenFK = table.Column<int>(type: "int", nullable: false),
                    IdTipoPerFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Ciudad_IdCiuFK",
                        column: x => x.IdCiuFK,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_Genero_IdGenFK",
                        column: x => x.IdGenFK,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_Tipo Persona_IdTipoPerFK",
                        column: x => x.IdTipoPerFK,
                        principalTable: "Tipo Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_IdDepFK",
                table: "Ciudad",
                column: "IdDepFK");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_IdPaisFK",
                table: "Departamento",
                column: "IdPaisFK");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdCiuFK",
                table: "Persona",
                column: "IdCiuFK");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdGenFK",
                table: "Persona",
                column: "IdGenFK");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdTipoPerFK",
                table: "Persona",
                column: "IdTipoPerFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Tipo Persona");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
