using Adapter_TCMB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<CurrencyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", async (CurrencyService currencyService) =>
{
    var currencies = await currencyService.GetCurrenciesAsync();
    return Results.Json(currencies);
});

app.MapGet("/convert/{from}/{to}/{amount}", (string from, string to, decimal amount) => new { from, to, amount });

app.Run();
