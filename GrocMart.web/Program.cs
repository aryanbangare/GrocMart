using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using GrocMart.Persistence;
using GrocMart.Services.Services;
using GrocMart.web.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));

builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "Grocery";
                
            });

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddScoped<UsersServices>()
    .AddScoped<ProductsServices>()
    .AddScoped<CartServices>()
    .AddScoped<OrdersServices>()
    .AddScoped<OrderItemServices>();

builder.Services.AddCors();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowAnyOrigin();
});

RouteGroupBuilder ApiGroup = app.MapGroup("api");

ApiGroup.MapUsersEndpoints()
        .MapProductsEndpoints()
        .MapCartEndpoints()
        .MapOrdersEndpoints()
        .MapOrderItemEndpoints();

app.MapGet("/", () => "Welcome To GrocMart Api!");
app.Run();