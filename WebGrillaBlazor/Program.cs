using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebGrillaBlazor;
using WebGrillaBlazor.ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient con la URL base
builder.Services.AddScoped(sp =>
{
    return new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7093/")
    };
});

// Registrar los clientes API
builder.Services.AddScoped<ApiClientTema>();
builder.Services.AddScoped<ApiClientSubtema>();
builder.Services.AddScoped<ApiClientRecurso>();
builder.Services.AddScoped(typeof(ApiClientGeneric<>));

builder.Services.AddBlazorBootstrap();

var app = builder.Build();


await app.RunAsync();