using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PocketBinder.Data;
using Refit;
using PocketBinder.Services.AuthServices;
using PocketBinder.Services.TcgApiServices;
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
builder.Services.AddSwaggerGen();

// Registramos el servicio de autenticación para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IAuthService, AuthService>();
// Registramos el servicio de Pokémon TCG para que pueda ser inyectado en los controladores
builder.Services.AddScoped<IPokemonTcgService, PokemonTcgService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Habilitamos la autenticación y autorización en el pipeline de la aplicación
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
