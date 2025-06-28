namespace api_delivery.Endpoints;

using System.Data;
using api_delivery.Models;
using Dapper;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginRequest request, IDbConnection db) => {
            
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            var user = await db.QueryFirstOrDefaultAsync<User>(sql, new { request.Username });

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Results.Unauthorized();
            }
            
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            return Results.Ok(new { Token = token, Username = user.Username });
        });
    }
}

public record LoginRequest(string Username, string Password);