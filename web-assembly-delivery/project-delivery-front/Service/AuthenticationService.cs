namespace project_delivery.Service;

using System.Net.Http.Json;
using System.Text.Json;

public class AuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public User? CurrentUser { get; private set; }
    public string? Token { get; private set; }
    public event Action? OnChange;

    public AuthenticationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var httpClient = _httpClientFactory.CreateClient("Api");
        try
        {
            var response = await httpClient.PostAsJsonAsync("login", new { Username = username, Password = password });
            if (!response.IsSuccessStatusCode) return false;

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (loginResult is null || string.IsNullOrEmpty(loginResult.Token)) return false;

            Token = loginResult.Token;
            CurrentUser = new User { Name = loginResult.Username };
            NotifyStateChanged(); 
            return true;
        }
        catch { return false; }
    }

    public void Logout()
    {
        CurrentUser = null;
        Token = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class User { public string Name { get; set; } = string.Empty; }
public class LoginResult { public string Token { get; set; } = string.Empty; public string Username { get; set; } = string.Empty; }
