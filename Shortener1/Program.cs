using ConsoleApp2205.Cofigs;
using ConsoleApp2205.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shortener1.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddScoped<SqlLightContext>();
builder.Services.AddScoped<IUrlMapRepository, UrlMapRepositoryImpl>();
builder.Services.AddScoped<IUrlMapService, UrlMapServiceImpl>();





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


app.UseRouting();
app.Run();
