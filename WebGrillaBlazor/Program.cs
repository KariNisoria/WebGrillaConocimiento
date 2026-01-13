using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebGrillaBlazor;
using WebGrillaBlazor.ApiClient;
using WebGrillaBlazor.Services;

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
builder.Services.AddScoped<ApiClientEvaluacion>();
builder.Services.AddScoped<ApiClientConocimiento>();
builder.Services.AddScoped(typeof(ApiClientGeneric<>));

// NUEVO: Registrar servicios de autenticación
builder.Services.AddScoped<ApiClientAuthentication>();
builder.Services.AddScoped<AuthStateService>();

builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Inicializar el servicio de autenticación al arrancar la aplicación
var authService = app.Services.GetRequiredService<AuthStateService>();
await authService.InitializeAsync();

await app.RunAsync();