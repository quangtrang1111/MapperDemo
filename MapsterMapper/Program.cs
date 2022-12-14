using MapperShared.DTOs;
using MapperShared.Models;
using MapperShared.Services;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetupMapperConfig();
builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

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

static void SetupMapperConfig()
{
    TypeAdapterConfig<WeatherForecastModel, WeatherForecastDto>.NewConfig()
        .Map(dest => dest.TemperatureF, src => src.TemperatureC);
}