using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notas.Server.Migrations
{
    /// <inheritdoc />
    public partial class NotasBuenas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sin categoria" },
                    { 2, "Ejercicio" },
                    { 3, "Diario" },
                    { 4, "Recetas" },
                    { 5, "Pendientes" },
                    { 6, "Compras pendientes" },
                    { 7, "Comandos .Net" }
                });

            migrationBuilder.InsertData(
                table: "Notas",
                columns: new[] { "Id", "Descripcion", "Fecha", "IdCategoria", "Titulo" },
                values: new object[,]
                {
                    { 1, "Hoy me senti muy bien fui por helado y luego comi pizza", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4182), 3, "Dia 16" },
                    { 2, "Realizar 5 repeticiones con 40 segundos de descanso entre repeticiones", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4223), 2, "Palanca" },
                    { 3, "Hacer 45 repeticiones minimo diario sobre la barra", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4251), 2, "Barra" },
                    { 4, "Pollo, panco, aceite, pimienta, chipotle, salsa de ajo, perejil", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4277), 4, "Milanesa" },
                    { 5, "Pastas, Pollo, crema de champiñones, pimenton, cebolla, pimienta, salsa, leche y crema de leche", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4304), 4, "Pastas en salsa de champiñones" },
                    { 6, "Arina, levadura, pasta de tomate", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4334), 4, "Masa de pizza" },
                    { 7, "add-migration (nombredelamigracion; update-database;)", new DateTime(2024, 4, 16, 14, 5, 25, 890, DateTimeKind.Local).AddTicks(4361), 7, "Migrar BD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notas_IdCategoria",
                table: "Notas",
                column: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
