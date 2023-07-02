using Gestion_Usuarios.Context;
using Gestion_Usuarios.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static Gestion_Usuarios.Services.IUsuariosService;

var builder = WebApplication.CreateBuilder(args);

// Configure services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(8, 0, 3))));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();