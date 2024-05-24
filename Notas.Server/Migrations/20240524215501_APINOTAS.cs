using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notas.Server.Migrations
{
    /// <inheritdoc />
    public partial class APINOTAS : Migration
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
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
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
                columns: new[] { "Id", "Email", "Nombre" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Sin categoria" },
                    { 2, "admin@gmail.com", "Ejercicio" },
                    { 3, "admin@gmail.com", "Diario" },
                    { 4, "admin@gmail.com", "Recetas" },
                    { 5, "admin@gmail.com", "Pendientes" },
                    { 6, "admin@gmail.com", "Compras pendientes" },
                    { 7, "admin@gmail.com", "Comandos .Net" }
                });

            migrationBuilder.InsertData(
                table: "Notas",
                columns: new[] { "Id", "Descripcion", "Fecha", "IdCategoria", "Titulo" },
                values: new object[,]
                {
                    { 1, "Hoy me senti muy bien fui por helado y luego comi pizza", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5077), 3, "Dia 16" },
                    { 2, "Realizar 5 repeticiones con 40 segundos de descanso entre repeticiones", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5141), 2, "Palanca" },
                    { 3, "Hacer 45 repeticiones minimo diario sobre la barra", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5169), 2, "Barra" },
                    { 4, "Pollo, panco, aceite, pimienta, chipotle, salsa de ajo, perejil", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5196), 4, "Milanesa" },
                    { 5, "Pastas, Pollo, crema de champiñones, pimenton, cebolla, pimienta, salsa, leche y crema de leche", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5224), 4, "Pastas en salsa de champiñones" },
                    { 6, "Arina, levadura, pasta de tomate", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5253), 4, "Masa de pizza" },
                    { 7, "add-migration (nombredelamigracion; update-database;)", new DateTime(2024, 5, 24, 16, 55, 0, 649, DateTimeKind.Local).AddTicks(5279), 7, "Migrar BD" }
                });

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
