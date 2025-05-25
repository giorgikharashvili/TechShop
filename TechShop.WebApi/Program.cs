using System.Reflection;
using TechShop.Application.Services;
using TechShop.Infrastructure.Repositories;

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

// Temporary database

builder.Services.AddSingleton<shopDatabase>();



// Repositories

builder.Services.AddScoped<AddressesRepository>();
builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<CartItemRepository>();
builder.Services.AddScoped<CategoriesRepository>();
builder.Services.AddScoped<OrderDetailsRepository>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<PaymentsRepository>();
builder.Services.AddScoped<ProductSkuAttributesRepository>();
builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddScoped<ProductsSkusRepository>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<WishlistRepository>();

// Services

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
