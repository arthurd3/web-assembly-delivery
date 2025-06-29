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

            // SQL ajustado para inserir apenas Username e PasswordHash
            var insertSql = @"
                INSERT INTO Users (Username, PasswordHash)
                VALUES (@Username, @PasswordHash);";
    
            // Objeto de parâmetro ajustado para não incluir o Email
            await db.ExecuteAsync(insertSql, new
            {
                request.Username,
                PasswordHash = passwordHash
            });

            return Results.Ok(new { Message = "Registo efetuado com sucesso!" });
        });
    }
}

// Record ajustado para requerer apenas Username e Password
public record RegisterRequest(
    [Required] string Username, 
    [Required] string Password
);