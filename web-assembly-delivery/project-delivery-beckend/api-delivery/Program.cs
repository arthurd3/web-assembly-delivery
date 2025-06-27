
var builder = WebApplication.CreateBuilder(args);

// serviço de CORS (essencial para a conexão)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontend", policy =>
    {
        // Permite que o seu frontend se conecte a esta API
        policy.WithOrigins("http://localhost:5041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// endpoint para o frontend testar
app.MapGet("/weatherforecast", () =>
{
    
    return new[] {
        new {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 22,
            Summary = "Conexão com o backend estabelecida com SUCESSO!"
        }
    };
});

app.Run();