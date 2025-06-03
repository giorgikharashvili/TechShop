using System.Reflection;
using System.Threading.RateLimiting;
using TechShop.Infrastructure;
using TechShop.Infrastructure.Repositories;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.WebAPI.Config;
using TechShop.Application;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
});

// Rate Limiter

#region RateLimiter
var rateLimitingSettings = builder.Configuration.GetSection("RateLimiter").Get<RateLimiterSettings>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("RequestsLimiter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            PermitLimit = rateLimitingSettings.MaxRequests,
            Window = TimeSpan.FromSeconds(rateLimitingSettings.WindowSeconds)
        }));
});
#endregion

builder.Services.AddScoped<IDbConnection>(db =>
{
    return new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));
});

// TechShop.Application Services

builder.Services.AddApplicationServices();


#region Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
