using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Ocelot yap�land�rmas�n� y�kle:
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// JWT kimlik do�rulamas�n� yap�land�r:
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer("GatewayAuthenticationScheme", options =>
//    {
//        options.Authority = "https://localhost:5001"; // IdentityServer URL'si
//        options.Audience = "api-gateway"; // Kim i�in kimlik do�rulamas� yap�lacaksa, bu API'nin Audience de�eri
//        options.RequireHttpsMetadata = false; // Geli�tirme ortam�nda HTTPS gereksinimini devre d��� b�rak
//    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Gateway",
        Version = "v1",
        Description = "API Gateway for microservices"
    });
});

// CORS politikalar�n� yap�land�r:
builder.Services.AddCors(options =>
{
    // T�m kaynaklara izin ver
    options.AddPolicy(
        "AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Ocelot'u ekle:
builder.Services.AddOcelot();
var app = builder.Build();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
    c.RoutePrefix = "swagger"; // Swagger UI'yi k�k dizine yerle�tir
});
app.UseAuthentication();
app.UseAuthorization();

app.UseHsts();

app.MapGet("/", () => Results.Ok("API Gateway running!"));

// Ocelot middleware'ini kullan:
await app.UseOcelot();

app.Run();
