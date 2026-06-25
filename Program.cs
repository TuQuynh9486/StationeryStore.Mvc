using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Options;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Options Pattern
builder.Services.Configure<StoreSettings>(
    builder.Configuration.GetSection("StoreSettings"));

// DbContext
builder.Services.AddDbContext<StationeryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Layer
builder.Services.AddScoped<IStationeryRepository,
                           StationeryRepository>();

builder.Services.AddScoped<IInventoryRepository,
                           InventoryRepository>();

// Service Layer
builder.Services.AddScoped<IStationeryService,
                           StationeryService>();

builder.Services.AddScoped<IInventoryService,
                           InventoryService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<
    ISupplierRepository,
    SupplierRepository>();

builder.Services.AddScoped<
    ISupplierService,
    SupplierService>();

builder.Services.AddScoped<
    IHealthService,
    HealthService>();

builder.Logging.AddConsole();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<StationeryDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHealthChecks("/health/live");

app.MapHealthChecks("/health/ready");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();