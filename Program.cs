using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Options;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


// ======================================================
// MVC + Razor Pages
// ======================================================

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();


// ======================================================
// Options Pattern
// ======================================================

builder.Services.Configure<StoreSettings>(
    builder.Configuration.GetSection("StoreSettings"));


// ======================================================
// Database
// ======================================================

builder.Services.AddDbContext<StationeryDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});


// ======================================================
// Identity
// ======================================================

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StationeryDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "CanViewProduct",
        policy =>
            policy.RequireRole("Admin", "Staff"));

    options.AddPolicy(
        "CanManageProduct",
        policy =>
            policy.RequireRole("Admin"));

    options.AddPolicy(
        "CanViewAuditLog",
        policy =>
            policy.RequireRole("Admin"));

    options.AddPolicy(
        "CanUploadProductImage",
        policy =>
            policy.RequireRole("Admin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
});


// ======================================================
// Repository
// ======================================================

builder.Services.AddScoped<IStationeryRepository, StationeryRepository>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


// ======================================================
// Service
// ======================================================

builder.Services.AddScoped<IStationeryService, StationeryService>();

builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IHealthService, HealthService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuditLogService, AuditLogService>();



// ======================================================
// Logging
// ======================================================

builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Logging.AddDebug();


// ======================================================
// Health Check
// ======================================================

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<StationeryDbContext>();



var app = builder.Build();


// ======================================================
// Seed Roles + Admin
// ======================================================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        services.GetRequiredService<UserManager<ApplicationUser>>();



    // ---------- Roles ----------

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(
            new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("Staff"))
    {
        await roleManager.CreateAsync(
            new IdentityRole("Staff"));
    }
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(
            new IdentityRole("User"));
    }



    // ---------- Admin ----------

    string email = "admin@gmail.com";

    string password = "Admin@123";

    var admin =
        await userManager.FindByEmailAsync(email);

    if (admin == null)
    {
        admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, password);

        if (!result.Succeeded)
        {
            throw new Exception("Không tạo được tài khoản Admin.");
        }
    }

    if (!await userManager.IsInRoleAsync(admin, "Admin"))
    {
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    var staff =
    await userManager.FindByEmailAsync("staff@gmail.com");

    if (staff == null)
    {
        staff = new ApplicationUser
        {
            UserName = "staff@gmail.com",
            Email = "staff@gmail.com",
            EmailConfirmed = true,
            FullName = "Staff User"
        };

        var result =
            await userManager.CreateAsync(
                staff,
                "Staff@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(
                staff,
                "Staff");
        }
    }
    var user =
    await userManager.FindByEmailAsync("user@gmail.com");

    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = "user@gmail.com",
            Email = "user@gmail.com",
            EmailConfirmed = true,
            FullName = "Normal User"
        };

        var result =
            await userManager.CreateAsync(
                user,
                "User@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(
                user,
                "User");
        }
    }
}



// ======================================================
// Middleware
// ======================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// ======================================================
// Health Check
// ======================================================

app.MapHealthChecks("/health/live");

app.MapHealthChecks(
    "/health/ready",
    new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType =
                "application/json";

            var result = new
            {
                status = report.Status.ToString(),

                totalChecks = report.Entries.Count,

                checks = report.Entries.Select(x => new
                {
                    name = x.Key,
                    status = x.Value.Status.ToString()
                }),

                application = "Stationery Store MVC",

                generatedAt = DateTime.UtcNow
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    result,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));
        }
    });


// ======================================================
// Routing
// ======================================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stationery}/{action=Dashboard}/{id?}");

app.MapRazorPages();

app.Run();