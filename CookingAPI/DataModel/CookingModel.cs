using CookingAPI.Constantes;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.DataModel
{
    public class CookingModel : DbContext
    {
        private readonly ILogger<CookingModel> _logger;

        public CookingModel(DbContextOptions<CookingModel> options, ILogger<CookingModel> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<IngredienteAlergeno> IngredienteAlergenos { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<RecetaIngrediente> RecetaIngredientes { get; set; }
        public DbSet<TipoDieta> TiposDieta { get; set; }
        public DbSet<TipoAlergeno> TiposAlergeno { get; set; }
        public DbSet<TipoIngrediente> TiposIngrediente { get; set; }
        public DbSet<TipoElaboracion> TiposElaboracion { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioReceta> UsuarioRecetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredienteAlergeno>()
                .Property(ur => ur.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<IngredienteAlergeno>()
                .HasKey(ri => new { ri.IdIngrediente, ri.IdTipoAlergeno });
            modelBuilder.Entity<IngredienteAlergeno>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany()
                .HasForeignKey(ri => ri.IdIngrediente);

            modelBuilder.Entity<IngredienteAlergeno>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany(r => r.IngredienteAlergenos)
                .HasForeignKey(ri => ri.IdTipoAlergeno);

            modelBuilder.Entity<RecetaIngrediente>()
                .Property(ur => ur.Id)
                .ValueGeneratedOnAdd();
            // Configuración de la relación entre Receta y RecetaIngrediente
            modelBuilder.Entity<RecetaIngrediente>()
                .HasKey(ri => new { ri.IdReceta, ri.IdIngrediente });

            modelBuilder.Entity<RecetaIngrediente>()
                .HasOne(ri => ri.Receta)
                .WithMany(r => r.RecetaIngredientes)
                .HasForeignKey(ri => ri.IdReceta);

            modelBuilder.Entity<RecetaIngrediente>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany()
                .HasForeignKey(ri => ri.IdIngrediente);

            // Configuración de la relación entre Ingrediente y IngredienteAlergeno
            modelBuilder.Entity<IngredienteAlergeno>()
                .HasKey(ia => new { ia.IdIngrediente, ia.IdTipoAlergeno });

            modelBuilder.Entity<IngredienteAlergeno>()
                .HasOne(ia => ia.Ingrediente)
                .WithMany(i => i.IngredienteAlergenos)
                .HasForeignKey(ia => ia.IdIngrediente);

            modelBuilder.Entity<IngredienteAlergeno>()
                .HasOne(ia => ia.TipoAlergeno)
                .WithMany()
                .HasForeignKey(ia => ia.IdTipoAlergeno);

            modelBuilder.Entity<UsuarioReceta>()
            .HasKey(ur => new { ur.UsuarioId, ur.RecetaId });

            modelBuilder.Entity<UsuarioReceta>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRecetas)
                .HasForeignKey(ur => ur.UsuarioId);

            modelBuilder.Entity<UsuarioReceta>()
                .HasOne(ur => ur.Receta)
                .WithMany(r => r.UsuarioRecetas)
                .HasForeignKey(ur => ur.RecetaId);

            modelBuilder.Entity<UsuarioReceta>()
                .Property(ur => ur.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);


            // Seed data para los enums
            modelBuilder.Entity<TipoDieta>().HasData(
                new TipoDieta { IdTipoDieta = 1, Nombre = "Vegana" },
                new TipoDieta { IdTipoDieta = 2, Nombre = "Pescetariana" },
                new TipoDieta { IdTipoDieta = 3, Nombre = "Lactovegetariana" },
                new TipoDieta { IdTipoDieta = 4, Nombre = "Lactoovovegetariana" },
                new TipoDieta { IdTipoDieta = 5, Nombre = "Ovovegetariana" },
                new TipoDieta { IdTipoDieta = 6, Nombre = "Vegetariana" },
                new TipoDieta { IdTipoDieta = 7, Nombre = "Omnívora" }
            );

            modelBuilder.Entity<TipoIngrediente>().HasData(
                new TipoIngrediente { IdTipoIngrediente = 1, Nombre = "Vegetal" },
                new TipoIngrediente { IdTipoIngrediente = 2, Nombre = "Carne" },
                new TipoIngrediente { IdTipoIngrediente = 3, Nombre = "Pescado" },
                new TipoIngrediente { IdTipoIngrediente = 4, Nombre = "Origen animal" },
                new TipoIngrediente { IdTipoIngrediente = 5, Nombre = "Condimento" }
            );

            modelBuilder.Entity<TipoAlergeno>().HasData(
                new TipoAlergeno { IdTipoAlergeno = 1, Nombre = "Gluten" },
                new TipoAlergeno { IdTipoAlergeno = 2, Nombre = "Crustáceos" },
                new TipoAlergeno { IdTipoAlergeno = 3, Nombre = "Huevos" },
                new TipoAlergeno { IdTipoAlergeno = 4, Nombre = "Pescado" },
                new TipoAlergeno { IdTipoAlergeno = 5, Nombre = "Cacahuetes" },
                new TipoAlergeno { IdTipoAlergeno = 6, Nombre = "Soja" },
                new TipoAlergeno { IdTipoAlergeno = 7, Nombre = "Leche" },
                new TipoAlergeno { IdTipoAlergeno = 8, Nombre = "Frutos con cáscara" },
                new TipoAlergeno { IdTipoAlergeno = 9, Nombre = "Apio" },
                new TipoAlergeno { IdTipoAlergeno = 10, Nombre = "Mostaza" },
                new TipoAlergeno { IdTipoAlergeno = 11, Nombre = "Sésamo" },
                new TipoAlergeno { IdTipoAlergeno = 12, Nombre = "Sulfitos" },
                new TipoAlergeno { IdTipoAlergeno = 13, Nombre = "Altramuces" },
                new TipoAlergeno { IdTipoAlergeno = 14, Nombre = "Moluscos" }
            );

            modelBuilder.Entity<TipoElaboracion>().HasData(
                new TipoElaboracion { IdTipoElaboracion = 1, Nombre = "Entrante" },
                new TipoElaboracion { IdTipoElaboracion = 2, Nombre = "Primer Plato" },
                new TipoElaboracion { IdTipoElaboracion = 3, Nombre = "Segundo Plato" },
                new TipoElaboracion { IdTipoElaboracion = 4, Nombre = "Postre" },
                new TipoElaboracion { IdTipoElaboracion = 5, Nombre = "Aperitivo" },
                new TipoElaboracion { IdTipoElaboracion = 6, Nombre = "Bebida" },
                new TipoElaboracion { IdTipoElaboracion = 7, Nombre = "Salsa" },
                new TipoElaboracion { IdTipoElaboracion = 8, Nombre = "Guarnición" }
            );

            // Seed data para Ingredientes
            modelBuilder.Entity<Ingrediente>().HasData(
                new Ingrediente { IdIngrediente = 1, Nombre = "Pasta", IdTipoIngrediente = 1 },
                new Ingrediente { IdIngrediente = 2, Nombre = "Pollo", IdTipoIngrediente = 2 },
                new Ingrediente { IdIngrediente = 3, Nombre = "Sal", IdTipoIngrediente = 5 },
                new Ingrediente { IdIngrediente = 4, Nombre = "Lechuga", IdTipoIngrediente = 1 },
                new Ingrediente { IdIngrediente = 5, Nombre = "Tomate", IdTipoIngrediente = 1 },
                new Ingrediente { IdIngrediente = 6, Nombre = "Pepino", IdTipoIngrediente = 1 },
                new Ingrediente { IdIngrediente = 7, Nombre = "Zanahoria", IdTipoIngrediente = 1 },
                new Ingrediente { IdIngrediente = 8, Nombre = "Aceite de oliva", IdTipoIngrediente = 5 }
            );

            // Seed data para Recetas
            modelBuilder.Entity<Receta>().HasData(
                new Receta { IdReceta = 1, IdTipoDieta = 7, Nombre = "Pasta con Pollo", Raciones = 4, Elaboracion = "Cocina la pasta y añade pollo.", Presentacion = "Servir caliente.", IdTipoElaboracion = 3, TiempoMinutos = 30 },
                new Receta { IdReceta = 2, IdTipoDieta = 1, Nombre = "Ensalada Vegana", Raciones = 2, Elaboracion = "Mezcla vegetales frescos.", Presentacion = "Servir fría.", IdTipoElaboracion = 1, TiempoMinutos = 15 }
            );

            // Seed data para RecetaIngredientes
            modelBuilder.Entity<RecetaIngrediente>().HasData(
                new RecetaIngrediente { IdReceta = 1, IdIngrediente = 1, Cantidad = 250 }, // Pasta
                new RecetaIngrediente { IdReceta = 1, IdIngrediente = 2, Cantidad = 300 }, // Pollo
                new RecetaIngrediente { IdReceta = 1, IdIngrediente = 3, Cantidad = 1 },    // Sal
                new RecetaIngrediente { IdReceta = 2, IdIngrediente = 4, Cantidad = 100 }, // Lechuga
                new RecetaIngrediente { IdReceta = 2, IdIngrediente = 5, Cantidad = 150 }, // Tomate
                new RecetaIngrediente { IdReceta = 2, IdIngrediente = 6, Cantidad = 100 }  // Pepino
            );

            // Log the database initialization
            _logger.LogInformation(Mensajes.Informacion.INICIALIZANDO);
        }
    }
}
