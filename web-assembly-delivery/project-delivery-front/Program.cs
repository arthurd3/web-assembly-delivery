using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using project_delivery;
using project_delivery.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// === SERVIÇOS DE AUTENTICAÇÃO E HTTP ===

builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


// === CONFIGURAÇÃO DO HTTPCLIENT ===

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5281");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));


// === OUTROS SERVIÇOS DA APLICAÇÃO ===
builder.Services.AddScoped<DeliveryService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<CartService>();


await builder.Build().RunAsync();
