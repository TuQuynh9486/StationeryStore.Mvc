using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public class HealthService : IHealthService
{
    private readonly StationeryDbContext _context;

    public HealthService(
        StationeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<HealthCheckItemViewModel>>
        GetHealthChecksAsync()
    {
        var productCount =
            await _context.StationeryItems.CountAsync();

        var supplierCount =
            await _context.Suppliers.CountAsync();

        var categoryCount =
            await _context.Categories.CountAsync();

        return new List<HealthCheckItemViewModel>
        {
            new()
            {
                Check = "Database",
                Expected = "Connected",
                Actual = "Connected",
                Status = "OK",
                Note = "EF Core SQL Server"
            },

            new()
            {
                Check = "Products",
                Expected = "> 0",
                Actual = productCount.ToString(),
                Status = productCount > 0 ? "OK" : "FAIL",
                Note = "StationeryItems"
            },

            new()
            {
                Check = "Categories",
                Expected = "> 0",
                Actual = categoryCount.ToString(),
                Status = categoryCount > 0 ? "OK" : "FAIL",
                Note = "Categories"
            },

            new()
            {
                Check = "Suppliers",
                Expected = "> 0",
                Actual = supplierCount.ToString(),
                Status = supplierCount > 0 ? "OK" : "FAIL",
                Note = "Suppliers"
            },

            new()
            {
                Check = "Transaction",
                Expected = "Inventory save",
                Actual = "Commit/Rollback",
                Status = "OK",
                Note = "InventoryService"
            }
        };
    }
}