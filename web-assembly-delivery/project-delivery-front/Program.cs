using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using project_delivery;
using project_delivery.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ESTA É A ÚNICA CONFIGURAÇÃO DE HTTPCLIENT QUE DEVE EXISTIR
builder.Services.AddScoped(sp => new HttpClient
{
    // Ela aponta corretamente para o seu backend
    BaseAddress = new Uri("http://localhost:5281")
});

//Injecao de Dependencia
builder.Services.AddSingleton<DeliveryService>();
builder.Services.AddSingleton<HomeService>();
builder.Services.AddSingleton<CartService>();

// A LINHA PROBLEMÁTICA FOI REMOVIDA DAQUI.

await builder.Build().RunAsync();