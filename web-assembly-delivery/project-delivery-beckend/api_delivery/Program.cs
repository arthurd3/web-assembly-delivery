using api_delivery.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5041")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapAuthenticationEndpoints();

app.Run();