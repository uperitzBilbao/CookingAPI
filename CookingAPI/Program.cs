using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Repositorio;
using CookingAPI.Respositorio;
using CookingAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar la cadena de conexión para el contexto de la base de datos
            builder.Services.AddDbContext<CookingModel>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CookingDatabase")));

            // Agregar servicios al contenedor.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registrar el servicio RecetaService y IngredienteService
            builder.Services.AddScoped<IRecetaService, RecetaService>();
            builder.Services.AddScoped<IIngredienteService, IngredienteService>();

            // Registrar los repositorios
            builder.Services.AddScoped<IRecetaRepositorio, RecetaRepositorio>(); // Si usas interfaz
            builder.Services.AddScoped<RecetaRepositorio>(); // Si no usas interfaz

            builder.Services.AddScoped<IIngredienteRepositorio, IngredienteRepositorio>();
            builder.Services.AddScoped<IngredienteRepositorio>();

            // Configurar el serializador para usar nombres de enums y preservar referencias
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            var app = builder.Build();

            // Verificar y aplicar las migraciones de la base de datos al iniciar la aplicación
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CookingModel>();
                    context.Database.Migrate(); // Aplica las migraciones pendientes y crea la base de datos si no existe
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error al aplicar las migraciones de la base de datos.");
                }
            }

            // Configurar la tubería de solicitudes HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
