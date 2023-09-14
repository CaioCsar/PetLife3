using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetLife.Data.Migrations
{
    public partial class Animal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    idAnimal = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    racaAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataNascAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pesoAnimal = table.Column<double>(type: "float", nullable: false),
                    tipoAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    caracteristicasAnimal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoMimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fotoAnimal = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.idAnimal);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    idMed = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeMed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoMed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataInicioMed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dosagemMed = table.Column<int>(type: "int", nullable: false),
                    periodoUsoMed = table.Column<int>(type: "int", nullable: false),
                    intervaloUsoMed = table.Column<int>(type: "int", nullable: false),
                    observacoesMed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idAnimal = table.Column<long>(type: "bigint", nullable: true),
                    AnimalidAnimal = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.idMed);
                    table.ForeignKey(
                        name: "FK_Medicamento_Animal_AnimalidAnimal",
                        column: x => x.AnimalidAnimal,
                        principalTable: "Animal",
                        principalColumn: "idAnimal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    idVacina = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeVacina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataAplicacaoVacina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    localAplicacaoVacina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataProximaVacina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observacaoVacina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fotoCertificadoVacina = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FotoMimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idAnimal = table.Column<long>(type: "bigint", nullable: true),
                    AnimalidAnimal = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacina", x => x.idVacina);
                    table.ForeignKey(
                        name: "FK_Vacina_Animal_AnimalidAnimal",
                        column: x => x.AnimalidAnimal,
                        principalTable: "Animal",
                        principalColumn: "idAnimal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_AnimalidAnimal",
                table: "Medicamento",
                column: "AnimalidAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_AnimalidAnimal",
                table: "Vacina",
                column: "AnimalidAnimal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "Vacina");

            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
