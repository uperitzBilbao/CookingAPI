﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposAlergeno",
                columns: table => new
                {
                    IdTipoAlergeno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAlergeno", x => x.IdTipoAlergeno);
                });

            migrationBuilder.CreateTable(
                name: "TiposDieta",
                columns: table => new
                {
                    IdTipoDieta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDieta", x => x.IdTipoDieta);
                });

            migrationBuilder.CreateTable(
                name: "TiposElaboracion",
                columns: table => new
                {
                    IdTipoElaboracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposElaboracion", x => x.IdTipoElaboracion);
                });

            migrationBuilder.CreateTable(
                name: "TiposIngrediente",
                columns: table => new
                {
                    IdTipoIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIngrediente", x => x.IdTipoIngrediente);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoDieta = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Raciones = table.Column<int>(type: "int", nullable: false),
                    Elaboracion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presentacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoElaboracion = table.Column<int>(type: "int", nullable: false),
                    TiempoMinutos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetas", x => x.IdReceta);
                    table.ForeignKey(
                        name: "FK_Recetas_TiposDieta_IdTipoDieta",
                        column: x => x.IdTipoDieta,
                        principalTable: "TiposDieta",
                        principalColumn: "IdTipoDieta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recetas_TiposElaboracion_IdTipoElaboracion",
                        column: x => x.IdTipoElaboracion,
                        principalTable: "TiposElaboracion",
                        principalColumn: "IdTipoElaboracion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoIngrediente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.IdIngrediente);
                    table.ForeignKey(
                        name: "FK_Ingredientes_TiposIngrediente_IdTipoIngrediente",
                        column: x => x.IdTipoIngrediente,
                        principalTable: "TiposIngrediente",
                        principalColumn: "IdTipoIngrediente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRecetas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RecetaId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRecetas", x => new { x.UsuarioId, x.RecetaId });
                    table.ForeignKey(
                        name: "FK_UsuarioRecetas_Recetas_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Recetas",
                        principalColumn: "IdReceta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRecetas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteAlergenos",
                columns: table => new
                {
                    IdIngrediente = table.Column<int>(type: "int", nullable: false),
                    IdTipoAlergeno = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAlergenoIdTipoAlergeno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteAlergenos", x => new { x.IdIngrediente, x.IdTipoAlergeno });
                    table.ForeignKey(
                        name: "FK_IngredienteAlergenos_Ingredientes_IdIngrediente",
                        column: x => x.IdIngrediente,
                        principalTable: "Ingredientes",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteAlergenos_TiposAlergeno_IdTipoAlergeno",
                        column: x => x.IdTipoAlergeno,
                        principalTable: "TiposAlergeno",
                        principalColumn: "IdTipoAlergeno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteAlergenos_TiposAlergeno_TipoAlergenoIdTipoAlergeno",
                        column: x => x.TipoAlergenoIdTipoAlergeno,
                        principalTable: "TiposAlergeno",
                        principalColumn: "IdTipoAlergeno");
                });

            migrationBuilder.CreateTable(
                name: "RecetaIngredientes",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdIngrediente = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaIngredientes", x => new { x.IdReceta, x.IdIngrediente });
                    table.ForeignKey(
                        name: "FK_RecetaIngredientes_Ingredientes_IdIngrediente",
                        column: x => x.IdIngrediente,
                        principalTable: "Ingredientes",
                        principalColumn: "IdIngrediente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaIngredientes_Recetas_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Recetas",
                        principalColumn: "IdReceta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TiposAlergeno",
                columns: new[] { "IdTipoAlergeno", "Nombre" },
                values: new object[,]
                {
                    { 1, "Gluten" },
                    { 2, "Crustáceos" },
                    { 3, "Huevos" },
                    { 4, "Pescado" },
                    { 5, "Cacahuetes" },
                    { 6, "Soja" },
                    { 7, "Leche" },
                    { 8, "Frutos con cáscara" },
                    { 9, "Apio" },
                    { 10, "Mostaza" },
                    { 11, "Sésamo" },
                    { 12, "Sulfitos" },
                    { 13, "Altramuces" },
                    { 14, "Moluscos" }
                });

            migrationBuilder.InsertData(
                table: "TiposDieta",
                columns: new[] { "IdTipoDieta", "Nombre" },
                values: new object[,]
                {
                    { 1, "Vegana" },
                    { 2, "Pescetariana" },
                    { 3, "Lactovegetariana" },
                    { 4, "Lactoovovegetariana" },
                    { 5, "Ovovegetariana" },
                    { 6, "Vegetariana" },
                    { 7, "Omnívora" }
                });

            migrationBuilder.InsertData(
                table: "TiposElaboracion",
                columns: new[] { "IdTipoElaboracion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Entrante" },
                    { 2, "Primer Plato" },
                    { 3, "Segundo Plato" },
                    { 4, "Postre" },
                    { 5, "Aperitivo" },
                    { 6, "Bebida" },
                    { 7, "Salsa" },
                    { 8, "Guarnición" }
                });

            migrationBuilder.InsertData(
                table: "TiposIngrediente",
                columns: new[] { "IdTipoIngrediente", "Nombre" },
                values: new object[,]
                {
                    { 1, "Vegetal" },
                    { 2, "Carne" },
                    { 3, "Pescado" },
                    { 4, "Origen animal" },
                    { 5, "Condimento" }
                });

            migrationBuilder.InsertData(
                table: "Ingredientes",
                columns: new[] { "IdIngrediente", "IdTipoIngrediente", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Pasta" },
                    { 2, 2, "Pollo" },
                    { 3, 5, "Sal" },
                    { 4, 1, "Lechuga" },
                    { 5, 1, "Tomate" },
                    { 6, 1, "Pepino" },
                    { 7, 1, "Zanahoria" },
                    { 8, 5, "Aceite de oliva" }
                });

            migrationBuilder.InsertData(
                table: "Recetas",
                columns: new[] { "IdReceta", "Elaboracion", "IdTipoDieta", "IdTipoElaboracion", "Nombre", "Presentacion", "Raciones", "TiempoMinutos" },
                values: new object[,]
                {
                    { 1, "Cocina la pasta y añade pollo.", 7, 3, "Pasta con Pollo", "Servir caliente.", 4, 30 },
                    { 2, "Mezcla vegetales frescos.", 1, 1, "Ensalada Vegana", "Servir fría.", 2, 15 }
                });

            migrationBuilder.InsertData(
                table: "RecetaIngredientes",
                columns: new[] { "IdIngrediente", "IdReceta", "Cantidad" },
                values: new object[,]
                {
                    { 1, 1, 250f },
                    { 2, 1, 300f },
                    { 3, 1, 1f },
                    { 4, 2, 100f },
                    { 5, 2, 150f },
                    { 6, 2, 100f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteAlergenos_IdTipoAlergeno",
                table: "IngredienteAlergenos",
                column: "IdTipoAlergeno");

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteAlergenos_TipoAlergenoIdTipoAlergeno",
                table: "IngredienteAlergenos",
                column: "TipoAlergenoIdTipoAlergeno");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_IdTipoIngrediente",
                table: "Ingredientes",
                column: "IdTipoIngrediente");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaIngredientes_IdIngrediente",
                table: "RecetaIngredientes",
                column: "IdIngrediente");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdTipoDieta",
                table: "Recetas",
                column: "IdTipoDieta");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdTipoElaboracion",
                table: "Recetas",
                column: "IdTipoElaboracion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRecetas_RecetaId",
                table: "UsuarioRecetas",
                column: "RecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredienteAlergenos");

            migrationBuilder.DropTable(
                name: "RecetaIngredientes");

            migrationBuilder.DropTable(
                name: "UsuarioRecetas");

            migrationBuilder.DropTable(
                name: "TiposAlergeno");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Recetas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TiposIngrediente");

            migrationBuilder.DropTable(
                name: "TiposDieta");

            migrationBuilder.DropTable(
                name: "TiposElaboracion");
        }
    }
}
