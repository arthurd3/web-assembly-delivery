using api_delivery.Database;
using api_delivery.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de autorização
builder.Services.AddAuthorization();

// Adiciona e configura a autenticação com JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration["Jwt:SecretKey"] ?? "uma-chave-secreta-muito-longa-e-segura-aqui-deve-ter-pelo-menos-32-bytes";
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Para simplificar, não validamos o emissor
            ValidateAudience = false, // Para simplificar, não validamos a audiência
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5041", "https://localhost:7041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuração do Dapper e do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=delivery.db";
builder.Services.AddScoped<IDbConnection>(_ => new SqliteConnection(connectionString));
builder.Services.AddScoped<DatabaseInitializer>();


// 2. Construção e Configuração do Pipeline da Aplicação
// =================================================

var app = builder.Build();

// Inicializa o banco de dados na primeira execução
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    dbInitializer.Initialize();
}

app.UseCors("AllowFrontend");

// IMPORTANTE: A ordem destes middlewares é crucial
app.UseAuthentication(); // 1º: Verifica se o pedido tem um token válido
app.UseAuthorization();  // 2º: Verifica se o utilizador autenticado tem permissão

// Mapeamento dos Endpoints
app.MapAuthenticationEndpoints();
app.MapRegisterEndpoints();
app.MapUserEndpoints(); 

app.Run();
