using System.Threading.RateLimiting;
using TechShop.Infrastructure.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.WebAPI.Config;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TechShop.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TechShop.Domain.Entities;
using Microsoft.OpenApi.Models;
using Stripe;
using TechShop.Application.Services.Interfaces;
using TechShop.Application.Services;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechShopWebAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter your JWT token with Bearer prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});


// Stripe 

#region Stripe
var stripeSecretkey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
StripeConfiguration.ApiKey = stripeSecretkey;

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ChargeService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<PriceService>();
#endregion
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

// Health Checks

#region HealthChecks
builder.Services.AddHealthChecks().AddSqlServer(
    connectionString: Environment.GetEnvironmentVariable("DB_CONNECTION"),
    name: "sqlserver",
    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy
    );
#endregion

// Authentication 

#region Authentication  
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
if (string.IsNullOrEmpty(jwtKey))
    throw new Exception("JWT Key is not configured.");

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = key
    };
});
#endregion

#region Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<PasswordHasher<Users>>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IStripeWebhookService, StripeWebhookService>();
builder.Services.AddScoped<IProductSkuRepository, ProductSkuRepository>();
builder.Services.AddScoped<IPaymentProcessingService, PaymentProcessingService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
builder.Services.AddScoped<IAddressesService, AddressesService>();
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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new {
                name = e.Key,
                status = e.Value.Status.ToString(),
                error = e.Value.Exception?.Message
            }),
            duration = report.TotalDuration.TotalSeconds
        });

        await context.Response.WriteAsync(result);
    }
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
