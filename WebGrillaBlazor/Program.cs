using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebGrillaBlazor;
using WebGrillaBlazor.ApiClient;
using WebGrillaBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🎯 CONFIGURACIÓN DE URL DINÁMICA
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:8080/";

// Configurar HttpClient con la URL base
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri(apiBaseUrl)
    };

    // Agregar headers necesarios
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

    return httpClient;
});

// Registrar los clientes API
builder.Services.AddScoped<ApiClientTema>();
builder.Services.AddScoped<ApiClientSubtema>();
builder.Services.AddScoped<ApiClientRecurso>();
builder.Services.AddScoped<ApiClientEvaluacion>();
builder.Services.AddScoped<ApiClientConocimiento>();
builder.Services.AddScoped(typeof(ApiClientGeneric<>));
builder.Services.AddScoped<ApiClientAuthentication>();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<ApiClientRecursoSupervisor>();

builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Log de configuración en consola (solo en desarrollo)
if (builder.HostEnvironment.IsDevelopment())
{
    Console.WriteLine($"🌐 Frontend configurado para conectar a: {apiBaseUrl}");
}

await app.RunAsync();