using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PocketBinder.Data;
using PocketBinder.Services.AlbumService;
using PocketBinder.Services.AuthServices;
using PocketBinder.Services.SyncService;
using PocketBinder.Services.TcgApiServices;
using PocketBinder.Services.UserCollectionService;
using Refit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT Authentication
builder.Services.AddAuthentication(options =>
{
    // Configuramos el esquema de autenticación por defecto para usar JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Configuración de validación del token JWT
    options.TokenValidationParameters = new TokenValidationParameters

    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero // Elimina el tiempo de tolerancia para la expiración del token
    };
});

// Configuración de autorización para proteger las rutas de la API
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
// Configuración de FluentValidation para validar los DTOs de entrada en los controladores
builder.Services.AddFluentValidationAutoValidation();

// Configuración de Entity Framework Core para usar SQL Server en desarrollo y MySQL/MariaDB en producción
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   {
     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

       if (builder.Environment.IsDevelopment())
       {
           // Desarrollo: SQL Server
           options.UseSqlServer(connectionString);
       }
       else
       {
           // Producción: MySQL/MariaDB
           var serverVersion = ServerVersion.AutoDetect(connectionString);
           options.UseMySql(connectionString, serverVersion);
       }
   });

// Configuración de Refit para consumir la API de Pokémon TCG
builder.Services.AddRefitClient<IPokemonTcgApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["PokemonTcgApi:BaseUrl"]);
        c.DefaultRequestHeaders.Add("X-Api-Key", builder.Configuration["PokemonTcgApi:ApiKey"]);
    });
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configuración de Swagger para incluir la autenticación JWT en la documentación de la API
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingresa tu token JWT: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuración de CORS para permitir solicitudes desde el frontend local solo en desarrollo
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("Development", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    }
});

// Registramos el servicio de autenticación para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IAuthService, AuthService>();
// Registramos el servicio de Pokémon TCG para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IPokemonTcgService, PokemonTcgService>();
// Registramos el servicio de sincronización entre la API y nuestra BBDD
builder.Services.AddScoped<ISyncService, SyncService>();
// Registramos el servicio de gestión de la colección del usuario para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IUserCollectionService, UserCollectionService>();
// Registramos el servicio de gestión de álbumes para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IAlbumService, AlbumService>();
// Registramos el servicio de sincronización con límite de tiempo
builder.Services.AddHostedService<SyncBackgroundService>();
// Registramos los validadores de FluentValidation para que puedan ser inyectados en los controladores
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// Registramos el servicio de acceso al contexto HTTP para que pueda ser inyectado en los servicios y controladores
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Habilitamos CORS solo en desarrollo para permitir solicitudes desde el frontend local
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}
// Habilitamos la autenticación y autorización en el pipeline de la aplicación
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
