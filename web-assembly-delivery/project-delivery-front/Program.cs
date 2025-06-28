using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using project_delivery;
using project_delivery.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5281")
});

//Injecao de Dependencia
builder.Services.AddScoped<DeliveryService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AuthenticationService>();

await builder.Build().RunAsync();