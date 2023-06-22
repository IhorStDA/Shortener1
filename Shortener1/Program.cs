using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shortener1;
using Shortener1.Data.Context;
using Shortener1.Entities;
using Shortener1.Repositories;
using Shortener1.Repositories.Implementations;
using Shortener1.Services;
using Shortener1.Services.BackgroundServices;
using Shortener1.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    
});

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(option => option.UseSqlite(connection));
builder.Services.AddDbContext<UserContext>(option => option.UseSqlite(connection));
        

builder.Services.AddScoped<IUrlMapRepository, UrlMapRepositoryImpl>();
builder.Services.AddScoped<IUrlMapService, UrlMapServiceImpl>();

builder.Services.AddScoped<IMarketingDataService, MarketingDataServiceImpl>();
builder.Services.AddScoped<IMarketingDataRepository, MarketingDataRepositoryImpl>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<AnalyticsBackgroundService>();
builder.Services.AddTransient<IServiceProvider>(provider => 
                                        provider.GetService<IServiceProviderFactory<IServiceCollection>>()
                                            .CreateServiceProvider(builder.Services));

builder.Services.AddValidatorsFromAssemblyContaining<UrlInputDtoValidator>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();



builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
    
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"]))
    
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseRouting();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseAuthentication(); 
    
    app.UseAuthorization();
    app.UseEndpoints(x => x.MapControllers()); 
    
}
else
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRouting();
    
    app.UseEndpoints(x => x.MapControllers());
    
}
app.UseHttpsRedirection();
app.Run();