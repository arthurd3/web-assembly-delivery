namespace project_delivery.Service;

using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;
    public AuthHeaderHandler(IServiceProvider serviceProvider) { _serviceProvider = serviceProvider; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Resolve o AuthenticationService aqui para quebrar o ciclo de dependência
        var authService = _serviceProvider.GetRequiredService<AuthenticationService>();

        if (!string.IsNullOrEmpty(authService.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authService.Token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}