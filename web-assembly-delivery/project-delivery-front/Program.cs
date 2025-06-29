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

// Configura um cliente HTTP "nomeado" chamado "Api".
// Este cliente terá o endereço base e o handler de autenticação.
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5281");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

// CORRIGIDO: Esta linha é crucial. Ela diz à aplicação que, sempre que
// um componente injetar um 'HttpClient' (como na sua página de registo),
// ele deve receber a instância configurada da factory com o nome "Api".
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));


// === OUTROS SERVIÇOS DA APLICAÇÃO ===
builder.Services.AddScoped<DeliveryService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<CartService>();


await builder.Build().RunAsync();
