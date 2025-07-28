using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register CarDataService and CarRepository
var carsJsonPath = Path.Combine(AppContext.BaseDirectory,"cars2025.json");
builder.Services.AddSingleton<CarCompare.Services.CarDataService>(_ => new CarCompare.Services.CarDataService(carsJsonPath));
builder.Services.AddSingleton(sp =>
{
    var carDataService = sp.GetRequiredService<CarCompare.Services.CarDataService>();
    var cars = carDataService.LoadCarsAsync().Result ?? new List<CarCompare.Models.CarModel>();
    return new CarCompare.Services.CarRepository(cars);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();


app.MapStaticAssets();

// Minimal API endpoints for car data
app.MapGet("/api/cars", (CarCompare.Services.CarRepository repo) =>
{
    var cars = repo.GetAll();
    var sb = new StringBuilder();
    foreach (var car in cars)
    {
        sb.Append("<tr>")
            .Append($"<td>{car.Make}</td>")
            .Append($"<td>{car.Model}</td>")
            .Append($"<td>{car.Year}</td>")
            .Append($"<td>{car.PriceMxn}</td>")
            .Append($"<td>{car.EngineSpecs?.Type ?? "-"}</td>")
            .Append($"<td>{car.FuelEfficiencyKml?.Combined ?? "-"}</td>")
            .Append($"<td>{(car.SafetyFeatures != null ? string.Join(", ", car.SafetyFeatures) : "-")}</td>")
            .Append("</tr>")
        ;
    }
    var html = sb.ToString();
    return Results.Content(html, "text/html");
});

app.MapGet("/api/cars/filter", (string? make, string? model, decimal? minPrice, decimal? maxPrice, CarCompare.Services.CarRepository repo) =>
{
    var cars = repo.Filter(make, model, minPrice, maxPrice);
    var sb = new StringBuilder();
    foreach (var car in cars)
    {
        sb.Append("<tr>")
            .Append($"<td>{car.Make}</td>")
            .Append($"<td>{car.Model}</td>")
            .Append($"<td>{car.Year}</td>")
            .Append($"<td>{car.PriceMxn}</td>")
            .Append($"<td>{car.EngineSpecs?.Type ?? "-"}</td>")
            .Append($"<td>{car.FuelEfficiencyKml?.Combined ?? "-"}</td>")
            .Append($"<td>{(car.SafetyFeatures != null ? string.Join(", ", car.SafetyFeatures) : "-")}</td>")
            .Append("</tr>");
    }
    var html = sb.ToString();
    return Results.Content(html, "text/html");
});

app.MapGet("/api/cars/filter", (string? make, string? model, decimal? minPrice, decimal? maxPrice, CarCompare.Services.CarRepository repo) =>
{
    var cars = repo.Filter(make, model, minPrice, maxPrice);
    var html = string.Join("\n", cars.Select(car => $"<tr>"
        + $"<td>{car.Make}</td>"
        + $"<td>{car.Model}</td>"
        + $"<td>{car.Year}</td>"
        + $"<td>{car.PriceMxn}</td>"
        + $"<td>{car.EngineSpecs?.Type ?? "-"}</td>"
        + $"<td>{car.FuelEfficiencyKml?.Combined ?? "-"}</td>"
        + $"<td>{(car.SafetyFeatures != null ? string.Join(", ", car.SafetyFeatures) : "-")}</td>"
        + "</tr>"));
    return Results.Content(html, "text/html");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
