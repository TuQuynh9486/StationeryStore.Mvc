using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Options;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.ViewModels;
using Microsoft.Extensions.Logging;

namespace StationeryStore.Mvc.Services;

public class StationeryService : IStationeryService
{
    private readonly IStationeryRepository _stationeryRepository;
    private readonly StoreSettings _settings;
    private readonly StationeryDbContext _context;
    private readonly ILogger<StationeryService> _logger;
    private readonly IAuditLogService _auditService;

    public StationeryService(
        IStationeryRepository stationeryRepository,
        IOptions<StoreSettings> options,
        StationeryDbContext context,
        ILogger<StationeryService> logger,
        IAuditLogService auditService)
    {
        _stationeryRepository = stationeryRepository;
        _settings = options.Value;
        _context = context;
        _logger = logger;
        _auditService = auditService;
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
            ImageUrl = item.ImageUrl,

            StockStatus =
        item.StockQuantity <= 0
            ? "Hết hàng"
            : item.StockQuantity <= _settings.LowStockThreshold
                ? "Sắp hết hàng"
                : "Còn hàng",

            StockStatusClass =
        item.StockQuantity <= 0
            ? "badge badge-danger"
            : item.StockQuantity <= _settings.LowStockThreshold
                ? "badge badge-warning"
                : "badge badge-success"
        }).ToList();
    }

    // =========================
    // DETAIL 
    // =========================
    public async Task<StationeryDetailViewModel?>
    GetDetailAsync(int id)
    {
        var item =
            await _stationeryRepository
                .GetByIdAsync(id);

        if (item == null)
            return null;

        return new StationeryDetailViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            Category = item.Category?.Name ?? "Chưa phân loại",
            Brand = item.Brand,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MinStock = item.MinStock,
            Description = item.Description,
            ImageUrl = item.ImageUrl,
            UpdatedAt = item.UpdatedAt,
        };
    }

    // =========================
    // CREATE 
    // =========================
    public async Task CreateAsync(
    StationeryCreateViewModel model)
    {
        using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var exists =
                await _stationeryRepository
                    .ExistsCodeAsync(model.Code);

            if (exists)
            {
                _logger.LogWarning(
                    "Create failed. Duplicate code {Code}.",
                    model.Code);

                throw new Exception(
                    "Mã sản phẩm đã tồn tại");
            }

            var entity = new StationeryItem
            {
                Code = model.Code,
                Name = model.Name,
                Brand = model.Brand,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                MinStock = model.MinStock,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };

            await _stationeryRepository.AddAsync(entity);

            await _stationeryRepository.SaveChangesAsync();

            await transaction.CommitAsync();

            _logger.LogInformation(
                "Created stationery {Code} - {Name}.",
                entity.Code,
                entity.Name);

            await _auditService.LogAsync(
                "Create",
                "StationeryItem",
                entity.Id,
                "Success",
                $"Created {entity.Code}");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            _logger.LogError(
                ex,
                "Create stationery failed. Code={Code}.",
                model.Code);

            throw;
        }
    }
    // =========================
    // SEARCH
    // =========================
    public async Task<List<StationeryListItemViewModel>> SearchAsync(
        StationerySearchViewModel model)
    {
        var items =
            await _stationeryRepository.SearchAsync(
                model.CategoryId,
                model.MinPrice,
                model.MaxPrice,
                model.Keyword);

        return items.Select(item => new StationeryListItemViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            Category = item.Category?.Name ?? "",
            Brand = item.Brand,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MinStock = item.MinStock,
            ImageUrl = item.ImageUrl,

            StockStatus =
            item.StockQuantity <= 0
                ? "Hết hàng"
                : item.StockQuantity <= _settings.LowStockThreshold
                    ? "Sắp hết hàng"
                    : "Còn hàng",

            StockStatusClass =
            item.StockQuantity <= 0
                ? "badge badge-danger"
                : item.StockQuantity <= _settings.LowStockThreshold
                    ? "badge badge-warning"
                    : "badge badge-success"
        }).ToList();
    }

    // =========================
    // DASHBOARD
    // =========================
    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            TotalProducts =
                await _context.StationeryItems.CountAsync(),

            TotalInventoryTransactions =
                await _context.InventoryRecords.CountAsync(),

            TotalAuditLogs =
                await _context.AuditLogs.CountAsync(),

            TotalCategories =
                await _context.Categories.CountAsync(),

            TotalSuppliers =
                await _context.Suppliers.CountAsync(),

            IdentityEnabled = true,

            AuthorizationEnabled = true,

            AntiForgeryEnabled = true,

            HealthCheckEnabled = true
        };
    }
    // =========================
    // EDIT
    // =========================
    public async Task<StationeryEditViewModel?> GetEditAsync(int id)
    {
        var item = await _stationeryRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return new StationeryEditViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            Brand = item.Brand,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            MinStock = item.MinStock,
            ImageUrl = item.ImageUrl,
            CategoryId = item.CategoryId,
            Description = item.Description,

            RowVersion = item.RowVersion ?? Array.Empty<byte>()
        };

    }
    // =========================
    // UPDATE
    // =========================
    public async Task UpdateAsync(
        StationeryEditViewModel model)
    {
        if (model.StockQuantity < 0)
        {
            throw new Exception(
                "Tồn kho không được nhỏ hơn 0");
        }
        var item =
            await _stationeryRepository
                .GetByIdAsync(model.Id);

        if (item == null)
            return;

        var exists =
            await _stationeryRepository
                .ExistsCodeExceptIdAsync(
                    model.Code,
                    model.Id);

        if (exists)
        {
            throw new Exception(
                "Mã sản phẩm đã tồn tại");
        }

        item.Code = model.Code;
        item.Name = model.Name;
        item.Brand = model.Brand;
        item.Price = model.Price;
        item.CategoryId = model.CategoryId;
        item.Description = model.Description;
        item.StockQuantity = model.StockQuantity;
        item.MinStock = model.MinStock;
        item.ImageUrl = model.ImageUrl;
        item.UpdatedAt = DateTime.UtcNow;
        item.RowVersion = model.RowVersion ?? Array.Empty<byte>();

        if (model.RowVersion == null)
        {
            throw new Exception("Dữ liệu không hợp lệ.");
        }

        _context.Entry(item)
    .Property(x => x.RowVersion)
    .OriginalValue = model.RowVersion;

        try
        {
            await _stationeryRepository.UpdateAsync(item);

            await _stationeryRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Updated stationery {Code}",
                item.Code);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(
                ex,
                "Concurrency conflict on {Code}",
                item.Code);

            throw new Exception(
                "Sản phẩm đã được người khác chỉnh sửa.");
        }
        await _auditService.LogAsync(
            "Edit",
            "StationeryItem",
            item.Id,
            "Success",
            $"Updated {item.Code}");
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(int id)
    {
        var item =
            await _stationeryRepository
                .GetByIdAsync(id);

        if (item == null)
        {
            _logger.LogWarning(
                "Delete failed. Item {Id} not found.",
                id);

            return;
        }

        item.IsDeleted = true;

        item.DeletedAt = DateTime.UtcNow;

        item.UpdatedAt = DateTime.UtcNow;

        await _stationeryRepository.UpdateAsync(item);

        await _stationeryRepository.SaveChangesAsync();

        _logger.LogWarning(
            "Soft deleted stationery {Code}",
            item.Code);

        await _auditService.LogAsync(
            "SoftDelete",
            "StationeryItem",
            item.Id,
            "Success",
            $"Deleted {item.Code}");

    }

    // =========================
    // RESTORE
    // =========================
    public async Task RestoreAsync(
        int id,
        byte[]? rowVersion)
    {
        if (rowVersion == null)
        {
            throw new Exception(
                "Không tìm thấy RowVersion");
        }

        try
        {
            await _stationeryRepository
                .RestoreAsync(id, rowVersion);

            await _stationeryRepository
                .SaveChangesAsync();

            _logger.LogInformation(
                "Restored stationery {Id}.",
                id);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception(
                "Dữ liệu đã bị thay đổi bởi người khác.");
        }

        await _auditService.LogAsync(
            "Restore",
            "StationeryItem",
            id,
            "Success",
            "Restore from trash");
    }
    // =========================
    // ADJUST STOCK
    // =========================
    public async Task<AdjustStockViewModel?> GetAdjustStockAsync(int id)
    {
        var item =
            await _stationeryRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return new AdjustStockViewModel
        {
            Id = item.Id,
            Code = item.Code,
            Name = item.Name,
            CurrentStock = item.StockQuantity,
            RowVersion = item.RowVersion ?? Array.Empty<byte>()
        };
    }

    public async Task AdjustStockAsync(
    AdjustStockViewModel model)
    {
        var item =
            await _stationeryRepository.GetByIdAsync(model.Id);

        if (item == null)
            return;

        var newStock =
            item.StockQuantity +
            model.ChangeQuantity;

        if (newStock < 0)
        {
            throw new Exception(
                "Tồn kho không được nhỏ hơn 0");
        }

        item.StockQuantity = newStock;

        item.UpdatedAt = DateTime.UtcNow;

        if (model.RowVersion == null)
        {
            throw new Exception("RowVersion không hợp lệ.");
        }


        _context.Entry(item)
            .Property(x => x.RowVersion)
            .OriginalValue = model.RowVersion;

        try
        {
            await _stationeryRepository.UpdateAsync(item);

            await _stationeryRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Adjusted stock {Code} by {Qty}",
                item.Code,
                model.ChangeQuantity);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception(
                "Dữ liệu đã bị thay đổi bởi người khác.");
        }

        await _auditService.LogAsync(
            "AdjustStock",
            "StationeryItem",
            item.Id,
            "Success",
            $"Change quantity = {model.ChangeQuantity}");
    }

    // =========================
    // TRASH
    // =========================
    public async Task<List<StationeryListItemViewModel>>
    GetTrashAsync()
    {
        var items =
            await _stationeryRepository.GetTrashAsync();

        return items.Select(item =>
            new StationeryListItemViewModel
            {
                Id = item.Id,
                RowVersion = item.RowVersion ?? Array.Empty<byte>(),
                Code = item.Code,
                Name = item.Name,
                Brand = item.Brand,
                Price = item.Price,
                StockQuantity = item.StockQuantity,
                MinStock = item.MinStock,
                ImageUrl = item.ImageUrl
            })
            .ToList();
    }
    // =========================
    // UPDATE IMAGE 
    // =========================
    public async Task UpdateImageAsync(
    int id,
    string imageUrl)
    {
        var item =
            await _stationeryRepository.GetByIdAsync(id);

        if (item == null)
            return;

        item.ImageUrl = imageUrl;

        item.LastUpdatedAt = DateTime.UtcNow;

        await _stationeryRepository.UpdateAsync(item);

        await _stationeryRepository.SaveChangesAsync();

        _logger.LogInformation(
            "Upload image for {Code}",
            item.Code);

        await _auditService.LogAsync(
            action: "UploadImage",
            entity: "StationeryItem",
            entityId: item.Id,
            result: "Success",
            detail: $"Image uploaded for {item.Code}");
    }
}