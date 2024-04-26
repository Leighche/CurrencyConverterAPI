var builder = WebApplication.CreateBuilder(args);

// Register the HTTP client with specific configuration
builder.Services.AddHttpClient("CurrencyConverter", client =>
{
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "b2eb59ffb5mshf61b0f9803743f6p196f01jsn7821bb865696");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "currency-exchange.p.rapidapi.com");
});

// Add other services
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
