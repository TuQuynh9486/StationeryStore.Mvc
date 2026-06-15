using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Options;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public class StationeryService : IStationeryService
{
    private readonly IStationeryRepository _stationeryRepository;
    private readonly StoreSettings _settings;
    private readonly StationeryDbContext _context;

    public StationeryService(
        IStationeryRepository stationeryRepository,
        IOptions<StoreSettings> options,
        StationeryDbContext context)
    {
        _stationeryRepository = stationeryRepository;
        _settings = options.Value;
        _context = context;
    }

    // =========================
    // GET ALL (INDEX)
    // =========================
    public async Task<List<StationeryListItemViewModel>> GetStationeryListAsync()
    {
        var items = await _stationeryRepository.GetAllReadOnlyAsync();

        return items.Select(item => new StationeryListItemViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            Category = item.Category != null ? item.Category.Name : "Chưa phân loại",
            Brand = item.Brand,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MinStock = item.MinStock,
            ImageUrl = item.ImageUrl
        }).ToList();
    }

    // =========================
    // CREATE (WITH TRANSACTION)
    // =========================
    public async Task CreateAsync(StationeryCreateViewModel model)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var entity = new StationeryItem
            {
                Code = model.Code,
                Name = model.Name,
                Brand = model.Brand,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                MinStock = model.MinStock,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };

            await _stationeryRepository.AddAsync(entity);

            entity.StockQuantity = entity.StockQuantity - 0;

            await _stationeryRepository.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // =========================
    // SEARCH
    // =========================
    public async Task<List<StationeryListItemViewModel>> SearchAsync(StationerySearchViewModel model)
    {
        var items = await _stationeryRepository.GetAllReadOnlyAsync();

        var query = items.AsQueryable();

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(x =>
                x.Name.Contains(model.Keyword) ||
                x.Code.Contains(model.Keyword));
        }

        if (!string.IsNullOrEmpty(model.Category))
        {
            query = query.Where(x =>
                x.Category != null &&
                x.Category.Name == model.Category);
        }

        if (model.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= model.MinPrice.Value);
        }

        if (model.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= model.MaxPrice.Value);
        }

        return query.Select(item => new StationeryListItemViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            Category = item.Category != null ? item.Category.Name : "N/A",
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MinStock = item.MinStock,
            ImageUrl = item.ImageUrl
        }).ToList();
    }

    public async Task<StationeryStatsViewModel> GetStatsAsync()
    {
        var items = await _stationeryRepository.GetAllReadOnlyAsync();

        return new StationeryStatsViewModel
        {
            TotalProducts = items.Count,

            TotalStockQuantity =
                items.Sum(x => x.StockQuantity),

            TotalInventoryValue =
                items.Sum(x => x.Price * x.StockQuantity),

            OutOfStockCount =
                items.Count(x => x.StockQuantity <= 0),

            LowStockCount =
                items.Count(x =>
                    x.StockQuantity > 0 &&
                    x.StockQuantity <= x.MinStock)
        };
    }

}