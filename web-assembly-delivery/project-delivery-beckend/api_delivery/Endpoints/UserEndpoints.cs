namespace api_delivery.Endpoints;

using System.Data;
using System.Security.Claims;
using api_delivery.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").RequireAuthorization();

        group.MapDelete("/me", async (IDbConnection db, HttpContext context) =>
        {
            var username = context.User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(username))
            {
                return Results.Unauthorized();
            }

            var sql = "DELETE FROM Users WHERE Username = @Username";
            var affectedRows = await db.ExecuteAsync(sql, new { Username = username });

            return affectedRows > 0 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteCurrentUser");


        group.MapPut("/update-password", async (UpdatePasswordRequest request, IDbConnection db, HttpContext context) =>
        {
            var username = context.User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(username))
            {
                return Results.Unauthorized();
            }
            
            if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 4)
            {
                return Results.BadRequest(new { Message = "A nova senha deve ter pelo menos 4 caracteres." });
            }

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            var sql = "UPDATE Users SET PasswordHash = @PasswordHash WHERE Username = @Username";
            var affectedRows = await db.ExecuteAsync(sql, new
            {
                PasswordHash = newPasswordHash,
                Username = username
            });

            return affectedRows > 0 ? Results.Ok(new { Message = "Senha atualizada com sucesso!" }) : Results.Problem("Não foi possível atualizar a senha.");
        })
        .WithName("UpdatePassword");
    }
}

public record UpdatePasswordRequest(string NewPassword);
