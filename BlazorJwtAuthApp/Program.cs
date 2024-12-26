using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BlazorJwtAuthApp",
            ValidAudience = "BlazorJwtAuthApp",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"))
        };
    });
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Настройка маршрутизации
app.UseRouting();

app.MapRazorPages();   // Настройка Razor Pages
app.MapBlazorHub();    // Настройка Blazor Hub

// Указание fallback маршрута для Blazor
app.MapFallbackToPage("/_Host");

app.Run();