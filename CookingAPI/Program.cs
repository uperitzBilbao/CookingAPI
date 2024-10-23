using CookingAPI.Authorization;
using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.ErrorHandler;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Repositorio;
using CookingAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Configuración de servicios
            builder.Services.AddDbContext<CookingModel>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CookingDatabase")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            // Registro de repositorios
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IUsuarioRecetaRepositorio, UsuarioRecetaRepositorio>();
            builder.Services.AddScoped<IRecetaRepositorio, RecetaRepositorio>();
            builder.Services.AddScoped<IRecetaIngredienteRepositorio, RecetaIngredienteRepositorio>();
            builder.Services.AddScoped<IIngredienteRepositorio, IngredienteRepositorio>();
            builder.Services.AddScoped<IIngredienteAlergenoRepositorio, IngredienteAlergenoRepositorio>();
            builder.Services.AddScoped<ITipoIngredienteRepositorio, TipoIngredienteRepositorio>();
            builder.Services.AddScoped<ITipoDietaRepositorio, TipoDietaRepositorio>();
            builder.Services.AddScoped<ITipoElaboracionRepositorio, TipoElaboracionRepositorio>();
            builder.Services.AddScoped<ITipoAlergenoRepositorio, TipoAlergenoRepositorio>();

            // Registro de servicios
            builder.Services.AddScoped<IRecetaService, RecetaService>();
            builder.Services.AddScoped<IIngredienteService, IngredienteService>();
            builder.Services.AddScoped<IRecetaIngredienteService, RecetaIngredienteService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IUsuarioRecetaService, UsuarioRecetaService>();
            builder.Services.AddScoped<IUserIdService, UserIdService>();

            // Configuración del serializador
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Configuración de autenticación JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

            // Configuración de la autorización personalizada
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                    policy.Requirements.Add(new CustomAuthorizationRequirement("custom_claim")));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            // Middleware de manejo de errores
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Aplicación de migraciones
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CookingModel>();
                    context.Database.Migrate();
                    logger.LogInformation(Mensajes.Informacion.MIGRACIONES_CON_EXITO);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, Mensajes.Error.ERROR_MIGRACIONES);
                }
            }

            // Configuración del pipeline HTTP
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
            logger.LogInformation(Mensajes.Informacion.API_LISTA);
            app.Run();
        }
    }
}
