namespace api_delivery.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
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
    }
}

public record LoginRequest(string Username, string Password);