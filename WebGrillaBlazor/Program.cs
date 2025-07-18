using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebGrillaBlazor;
using WebGrillaBlazor.ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Agregado de componentes al contenedor de dependencias

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new HttpClient
    {
        BaseAddress = new Uri(config["ApiSettings:BaseUrl"])
    };
});


// Registrar el cliente genérico
builder.Services.AddScoped<ApiClientTema>();
builder.Services.AddScoped<ApiClientSubtema>();
builder.Services.AddScoped<ApiClientRecurso>();
builder.Services.AddSingleton(typeof(ApiClientGeneric<>));

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
