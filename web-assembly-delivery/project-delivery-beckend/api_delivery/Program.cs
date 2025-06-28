using api_delivery.Database;
using api_delivery.Endpoints;
using Microsoft.Data.Sqlite;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5041", "https://localhost:7041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 2. Configuração do Dapper
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=delivery.db";
builder.Services.AddScoped<IDbConnection>(sp => new SqliteConnection(connectionString));
// CORRIGIDO: Alterado de Singleton para Scoped
builder.Services.AddScoped<DatabaseInitializer>();

var app = builder.Build();

// 3. Inicialização do Banco de Dados
// CORRIGIDO: O inicializador agora é chamado dentro de um escopo de serviço
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    dbInitializer.Initialize();
}


app.UseCors("AllowFrontend");

// 4. Mapeamento dos Endpoints
app.MapAuthenticationEndpoints();
app.MapRegisterEndpoints();

app.Run();
