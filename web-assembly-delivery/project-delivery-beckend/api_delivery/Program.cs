using api_delivery.Database;
using api_delivery.Endpoints;
using Microsoft.Data.Sqlite;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5041", "https://localhost:7041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuração do Dapper
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=delivery.db";
builder.Services.AddScoped<IDbConnection>(sp => new SqliteConnection(connectionString));
// CORRIGIDO: Alterado de Singleton para Scoped
builder.Services.AddScoped<DatabaseInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    dbInitializer.Initialize();
}

app.UseCors("AllowFrontend");

// Mapeamento dos Endpoints
app.MapAuthenticationEndpoints();
app.MapRegisterEndpoints();

app.Run();
