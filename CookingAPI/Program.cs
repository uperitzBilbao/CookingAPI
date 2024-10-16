using CookingAPI.Authorization;
using CookingAPI.DataModel;
using CookingAPI.ErrorHandler;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Repositorio;
using CookingAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization; // Importa este espacio de nombres
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Text.Json.Serialization;

namespace CookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt",
                              rollingInterval: RollingInterval.Day,
                              shared: true,
                              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            builder.Host.UseSerilog();

            // Configurar el logging en toda la aplicación
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Configurar la cadena de conexión para el contexto de la base de datos
            builder.Services.AddDbContext<CookingModel>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CookingDatabase")));

            // Agregar servicios al contenedor.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registro de repositorios
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IUsuarioRecetaRepositorio, UsuarioRecetaRepositorio>();
            builder.Services.AddScoped<IRecetaRepositorio, RecetaRepositorio>();
            builder.Services.AddScoped<IIngredienteRepositorio, IngredienteRepositorio>();

            // Registro de servicios 
            builder.Services.AddScoped<IRecetaService, RecetaService>();
            builder.Services.AddScoped<IIngredienteService, IngredienteService>();

            // Configurar el serializador para usar nombres de enums y preservar referencias
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Configurar autenticación JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // Registrar el handler y la política de autorización personalizada
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                    policy.Requirements.Add(new CustomAuthorizationRequirement("custom_claim")));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

            var app = builder.Build();

            // Añadir el middleware de manejo de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Obtener el logger global y pasar a las migraciones
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            // Aplicar migraciones de base de datos con el logger global
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CookingModel>();
                    logger.LogInformation("Iniciando migraciones de la base de datos...");

                    context.Database.Migrate();
                    logger.LogInformation("Migraciones de la base de datos aplicadas con éxito.");
                }
                catch (Exception ex)
                {
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.MapControllers();
            logger.LogInformation("API en funcionamiento...");
            app.Run();
        }
    }
}
