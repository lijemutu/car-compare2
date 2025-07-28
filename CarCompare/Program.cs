using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
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
app.UseSession();
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
            .Append($"<td>{car.PriceMxn:C0}</td>") // Format as currency
            .Append($"<td>{car.EngineSpecs?.Type ?? "-"}</td>")
            .Append($"<td>{car.FuelEfficiencyKml?.Combined ?? "-"}</td>")
            .Append($"<td>{(car.SafetyFeatures != null ? string.Join(", ", car.SafetyFeatures) : "-")}</td>")
            .Append($"<td><button class=\"add-to-compare px-4 py-2 text-sm font-medium text-white bg-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500\" data-car-id=\"{car.Id}\">Add to Compare</button></td>")
            .Append("</tr>")
        ;
    }
    var html = sb.ToString();
    return Results.Content(html, "text/html");
});

app.MapGet("/api/cars/filter", (string? make, string? model, decimal? minPrice, decimal? maxPrice, string? sortBy, CarCompare.Services.CarRepository repo) =>
{
    var cars = repo.Filter(make, model, minPrice, maxPrice, sortBy);
    var sb = new StringBuilder();
    foreach (var car in cars)
    {
        sb.Append("<tr>")
            .Append($"<td>{car.Make}</td>")
            .Append($"<td>{car.Model}</td>")
            .Append($"<td>{car.Year}</td>")
            .Append($"<td>{car.PriceMxn:C0}</td>") // Format as currency
            .Append($"<td>{car.EngineSpecs?.Type ?? "-"}</td>")
            .Append($"<td>{car.FuelEfficiencyKml?.Combined ?? "-"}</td>")
            .Append($"<td>{(car.SafetyFeatures != null ? string.Join(", ", car.SafetyFeatures) : "-")}</td>")
            .Append($"<td><button class=\"add-to-compare px-4 py-2 text-sm font-medium text-white bg-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500\" data-car-id=\"{car.Id}\">Add to Compare</button></td>")
            .Append("</tr>");
    }
    var html = sb.ToString();
    return Results.Content(html, "text/html");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
