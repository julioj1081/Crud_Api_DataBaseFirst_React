using ApiCoreID.Middleware;
using ApiCoreID.Models;
using ApiCoreID.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
/////// Instalar
//dotnet package add Microsoft.AspNetCore.OpenApi
//dotnet package add Microsoft.EntityFrameworkCore
//dotnet package add Microsoft.EntityFrameworkCore.SqlServer
//dotnet package add Microsoft.EntityFrameworkCore.Tools
//dotnet package add Swashbuckle.AspNetCore
//dotnet package add Microsoft.EntityFrameworkCore.Design



builder.Services.AddControllers();

//*********************************** Agregar
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<PhotosServices>();
builder.Services.AddScoped<GuidServices>(); //Middleware No es necesario solo sirve para ver como se ejecuta injeccion de dependencias

//Enable cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("*", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
    });
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FotosCrudContext>(options =>
{
    options.UseSqlServer(connection);
});

var app = builder.Build();


//*********************************** Agregar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Enable cors
app.UseCors("*");

app.UseHttpsRedirection();

//Middleware No es necesario solo sirve para ver como se ejecuta injeccion de dependencias
app.UseMiddleware<EjMiddleware>();

app.UseAuthorization();

app.MapControllers();

//*********************************** Agregar
// Redireccionar la raíz a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));



app.Run();

//Database First
//Despues de compilar todo agregar este comando en la terminal
//
//PM> Scaffold-DbContext "Server=ALBERTO;Database=FotosCrud;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
//PM> Scaffold-DbContext "Server=ALBERTO;Database=FotosCrud;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

