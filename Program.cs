using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuotationWritingSystem.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Get the connection string based on the environment (Development or Production)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Set frontend URL dynamically based on environment
var frontendUrl = builder.Configuration.GetValue<string>("FrontendUrls:Development");
if (builder.Environment.IsProduction())
{
    frontendUrl = builder.Configuration.GetValue<string>("FrontendUrls:Production");
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(frontendUrl) // Use the correct frontend URL for each environment
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure JWT authentication using TokenValidationParameters (with dynamic settings)
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new ArgumentNullException("Jwt:Key, Jwt:Issuer, or Jwt:Audience is missing in appsettings.json.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();  // Add authorization services

builder.Services.AddLogging(options =>
{
    options.AddConsole();  // Add console logging
});

var app = builder.Build();

// Configure the HTTP request pipeline based on the environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // In production, ensure we have security headers and logging
    app.UseHsts();
}

app.UseCors("AllowReactApp"); // Use the React app CORS policy

app.UseAuthentication(); // Ensure authentication is enabled
app.UseAuthorization();  // Ensure authorization is enabled

app.MapControllers(); // Map all API controllers

app.Run();
