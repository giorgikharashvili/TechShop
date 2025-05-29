using System.Reflection;
using System.Threading.RateLimiting;
using TechShop.Application.Services;
using TechShop.Infrastructure;
using TechShop.Infrastructure.Repositories;
using Dapper;


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

builder.Services.AddInfrastructure();


// Rate Limiter

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("RequestsLimiter", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(10)
        }));
});








#region Services
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

app.UseAuthorization();

app.MapControllers();

app.Run();
