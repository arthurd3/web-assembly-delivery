using System.Net.Http.Json;
using System.Text.Json;

namespace project_delivery.Service; 

public class AuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public User? CurrentUser { get; private set; }


    public event Action? OnChange;


    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var loginRequest = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
            
                CurrentUser = new User { Name = username };
                NotifyStateChanged(); 
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class User
{
    public string Name { get; set; } = string.Empty;
}