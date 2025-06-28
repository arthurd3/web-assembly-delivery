namespace api_delivery.Endpoints;

using System.ComponentModel.DataAnnotations;
using System.Data;
using api_delivery.Models;
using Dapper;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (RegisterRequest request, IDbConnection db) => 
        {
            var checkUserSql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            var userExists = await db.ExecuteScalarAsync<int>(checkUserSql, new { request.Username });

            if (userExists > 0)
            {
                return Results.Conflict(new { Message = "Este nome de utilizador já está em uso." });
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var insertSql = @"
                INSERT INTO Users (Username, PasswordHash, Email)
                VALUES (@Username, @PasswordHash, @Email);";
    
            await db.ExecuteAsync(insertSql, new
            {
                request.Username,
                PasswordHash = passwordHash,
                request.Email
            });

            return Results.Ok(new { Message = "Registo efetuado com sucesso!" });
        });
    }
}

public record RegisterRequest(
    [Required] string Username, 
    [Required] string Password, 
    string? Email
);
