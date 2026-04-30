using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using WebGrilla.Data;
using WebGrilla.Mappings;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar encoding UTF-8
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;


// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 🎯 OBTENER IP LOCAL DE LA MÁQUINA
string localIP = GetLocalIPAddress();

//----------------
// CONF. CORS - Permitir acceso desde cualquier origen en la red
//----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🎯 CONFIGURAR KESTREL PARA ESCUCHAR EN IP LOCAL Y LOCALHOST
builder.WebHost.ConfigureKestrel(options =>
{
    // Escuchar en localhost:8080 (para desarrollo local)
    options.Listen(IPAddress.Loopback, 8080);

    // 🌐 Escuchar en la IP local:8080 (para acceso desde red)
    if (IPAddress.TryParse(localIP, out var ipAddress))
    {
        options.Listen(ipAddress, 8080);
    }

    // También escuchar en todas las interfaces (0.0.0.0)
    options.Listen(IPAddress.Any, 8080);
});

// Resto de tu configuración actual...
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null
        );
    }
    ));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tu configuración actual de servicios...
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IRepository<Tema>, TemaRepository>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IRepository<Subtema>, SubtemaRepository>();
builder.Services.AddScoped<ISubtemaRepository, SubtemaRepository>();
builder.Services.AddScoped<ISubtemaService, SubtemaService>();
builder.Services.AddScoped<IRepository<Cliente>, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IRepository<TipoDocumento>, TipoDocumentoRepository>();
builder.Services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
builder.Services.AddScoped<IRepository<EquipoDesarrollo>, EquipoDesarrolloRepository>();
builder.Services.AddScoped<IEquipoDesarrolloService, EquipoDesarrolloService>();
builder.Services.AddScoped<IRepository<Recurso>, RecursoRepository>();
builder.Services.AddScoped<IRecursoRepository, RecursoRepository>();
builder.Services.AddScoped<IRecursoService, RecursoService>();
builder.Services.AddScoped<IRepository<Grilla>, GrillaRepository>();
builder.Services.AddScoped<IGrillaService, GrillaService>();
builder.Services.AddScoped<IRepository<GrillaTema>, GrillaTemaRepository>();
builder.Services.AddScoped<IGrillaTemaService, GrillaTemaService>();
builder.Services.AddScoped<IRepository<GrillaSubtema>, GrillaSubtemaRepository>();
builder.Services.AddScoped<IGrillaSubtemaRepository, GrillaSubtemaRepository>();
builder.Services.AddScoped<IGrillaSubtemaService, GrillaSubtemaService>();
builder.Services.AddScoped<IRepository<Evaluacion>, EvaluacionRepository>();
builder.Services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
builder.Services.AddScoped<IEvaluacionService, EvaluacionService>();
builder.Services.AddScoped<IRepository<ConocimientoRecurso>, ConocimientoRecursoRepository>();
builder.Services.AddScoped<IConocimientoRecursoRepository, ConocimientoRecursoRepository>();
builder.Services.AddScoped<IConocimientoRecursoService, ConocimientoRecursoService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRecursoSupervisorRepository, RecursoSupervisorRepository>();
builder.Services.AddScoped<IRecursoSupervisorService, RecursoSupervisorService>();

var app = builder.Build();

//------
// Cors
//------
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🎯 MOSTRAR INFORMACIÓN DE CONEXIÓN
Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
Console.WriteLine("║         🚀 API WebGrilla INICIADA                         ║");
Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
Console.WriteLine($"📡 Acceso Local:      http://localhost:8080");
Console.WriteLine($"🌐 Acceso en Red:     http://{localIP}:8080");
Console.WriteLine($"📚 Swagger (Local):   http://localhost:8080/swagger");
Console.WriteLine($"📚 Swagger (Red):     http://{localIP}:8080/swagger");
Console.WriteLine($"⏰ Iniciada:          {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
Console.WriteLine("════════════════════════════════════════════════════════════");
Console.WriteLine($"💡 Comparte esta URL con otros PCs: http://{localIP}:8080");
Console.WriteLine("════════════════════════════════════════════════════════════");

app.UseAuthorization();
app.MapControllers();

app.Run();

// 🎯 MÉTODO PARA OBTENER LA IP LOCAL
static string GetLocalIPAddress()
{
    try
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "127.0.0.1";
    }
    catch
    {
        return "127.0.0.1";
    }
}