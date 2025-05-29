using System.Reflection;
using System.Threading.RateLimiting;
using TechShop.Application.Services;
using TechShop.Infrastructure;
using TechShop.Infrastructure.Repositories;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.WebAPI.Config;


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




builder.Services.AddScoped(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
    return new SqlConnection(connectionString);
});

builder.Services.AddSingleton<IDbConnection>(db =>
{
    return new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));
});


#region Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<AddressesService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<OrderDetailsService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<PaymentsService>();
builder.Services.AddScoped<ProductSkuAttributesService>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<ProductsSkusService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<WishlistService>();
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
