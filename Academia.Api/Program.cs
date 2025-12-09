using Academia.Api.Repositories;
using Academia.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AcademiaDB");


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<ILeccionRepository, LeccionRepository>();
builder.Services.AddScoped<ILeccionService, LeccionService>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
builder.Services.AddScoped<IOpinionRepository, OpinionRepository>();
builder.Services.AddScoped<IOpinionService, OpinionService>();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Añadir servicios CORS (¡MOVIDO AQUÍ!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            // Permitir cualquier origen (solución para desarrollo local)
            policy.AllowAnyOrigin()
            // Permitir cualquier método HTTP (GET, POST, PUT, DELETE)
            .AllowAnyMethod()
            // Permitir cualquier encabezado en la solicitud
            .AllowAnyHeader();
        });
});


var app = builder.Build();

// ACTIVACIÓN DEL MIDDLEWARE CORS
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
