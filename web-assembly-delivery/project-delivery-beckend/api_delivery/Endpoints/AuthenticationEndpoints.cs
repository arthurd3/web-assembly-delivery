namespace api_delivery.Endpoints;

using System.Data;
using System.Security.Claims;
using System.Text;
using api_delivery.Models;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginRequest request, IDbConnection db, IConfiguration config) => {
            
            var user = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Username = @Username", new { request.Username });

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Results.Unauthorized();
            }
            
            var token = GenerateJwtToken(user, config);
            
            return Results.Ok(new { Token = token, Username = user.Username });
        });
    }

    private static string GenerateJwtToken(User user, IConfiguration config)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = config["Jwt:SecretKey"] ?? "uma-chave-secreta-muito-longa-e-segura-aqui-deve-ter-pelo-menos-32-bytes";
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public record LoginRequest(string Username, string Password);