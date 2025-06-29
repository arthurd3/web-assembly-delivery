namespace project_delivery.Service;

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationService _authService;
    public CustomAuthenticationStateProvider(AuthenticationService authService)
    {
        _authService = authService;
        _authService.OnChange += NotifyAuthenticationStateChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _authService.CurrentUser;
        if (user is null)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Name) }, "apiauth");
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}