using Microsoft.OpenApi.Models;
using PixAPI;
using PixAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = $"v{typeof(Program).Assembly.GetName().Version?.ToString()} " +
            $"{(builder.Configuration["Ambiente"] == "1" ? "Produção" : "Desenvolvimento")}",
        Title = "PixAPI",
        Description = "API simples que simula transações via PIX"
    });
});

builder.ConfigureDI();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
