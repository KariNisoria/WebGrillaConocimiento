using Microsoft.EntityFrameworkCore;
using WebGrilla.Data;
using WebGrilla.Models;
using WebGrilla.Repository;
using WebGrilla.Services;

var builder = WebApplication.CreateBuilder(args);

//----------------
// CONF. CORS
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


//--------------------
// GESTION DE CONEXION
//--------------------

// Add services to the container.

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


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----------------------------------
// CARGA DE DEPENDENCIAS FUNCIONALES
//----------------------------------

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

// Configuracion de reposiorios y servicios
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
