
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

app.MapPost("/login", (LoginRequest request) => {
    
    if (request.Username == "admin" && request.Password == "123")
    {
        
        return Results.Ok(new { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" });
    }
    else
    {
        
        return Results.Unauthorized();
    }
});


app.Run();


public record LoginRequest(string Username, string Password);