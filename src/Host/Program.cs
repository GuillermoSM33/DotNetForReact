using ApplicationCore;
using ApplicationCore.Interfaces;
using Infraestructure;
using Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddScoped<IColaboradoresService, ColaboradoresService>();

var app = builder.Build();
await app.Services.InitializeDatabasesAsync();

// Use CORS policy
app.UseCors("AllowReactApp");

app.UseInfraestructure();
app.Run();
