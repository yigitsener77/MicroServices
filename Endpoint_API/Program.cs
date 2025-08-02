using Endpoint_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the CalculatorService
builder.Services.AddSingleton<CalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define the calculator endpoints
app.MapGet("/add/{a}/{b}", (int a, int b, CalculatorService calculator) =>
{
    return Results.Ok(calculator.Add(a, b));
});

app.MapGet("/subtract/{a}/{b}", (int a, int b, CalculatorService calculator) =>
{
    return Results.Ok(calculator.Subtract(a, b));
});

app.MapGet("/multiply/{a}/{b}", (int a, int b, CalculatorService calculator) =>
{
    return Results.Ok(calculator.Multiply(a, b));
});

app.MapGet("/divide/{a}/{b}", (int a, int b, CalculatorService calculator) =>
{
    try
    {
        return Results.Ok(calculator.Divide(a, b));
    }
    catch (DivideByZeroException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();
