using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public class InventoryService : IInventoryService
{
    private readonly StationeryDbContext _context;
    private readonly IAuditLogService _auditService;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        StationeryDbContext context,
        IAuditLogService auditService,
        ILogger<InventoryService> logger)
    {
        _context = context;
        _auditService = auditService;
        _logger = logger;
    }

    // =========================
    // CREATE INVENTORY RECORD
    // =========================
    public async Task CreateInventoryRecordAsync(
        int stationeryItemId,
        int quantity,
        string note)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var item = await _context.StationeryItems
                .FirstOrDefaultAsync(x => x.Id == stationeryItemId);

            if (item == null)
            {
                throw new Exception("Không tìm thấy sản phẩm.");
            }

            var record = new InventoryRecord
            {
                CreatedAt = DateTime.UtcNow,
                Note = note
            };

            _context.InventoryRecords.Add(record);

            await _context.SaveChangesAsync();

            var detail = new InventoryDetail
            {
                InventoryRecordId = record.Id,
                StationeryItemId = item.Id,
                Quantity = quantity
            };

            _context.InventoryDetails.Add(detail);

            // Tracking Entity
            item.StockQuantity += quantity;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            _logger.LogInformation(
                "Inventory transaction created. Product={Code}, Quantity={Quantity}",
                item.Code,
                quantity);

            await _auditService.LogAsync(
                "Create Inventory",
                "InventoryRecord",
                record.Id,
                "Success",
                $"Product={item.Code}, Quantity={quantity}");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            _logger.LogError(
                ex,
                "Create inventory transaction failed.");

            await _auditService.LogAsync(
                "Create Inventory",
                "InventoryRecord",
                null,
                "Failed",
                ex.Message);

            throw;
        }
    }

    // =========================
    // INVENTORY HISTORY
    // =========================
    public async Task<List<InventoryRecord>> GetInventoryHistoryAsync()
    {
        return await _context.InventoryRecords
            .Include(x => x.InventoryDetails)
                .ThenInclude(d => d.StationeryItem)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<InventoryRecord>> GetAllAsync()
    {
        return await _context.InventoryRecords
            .Include(x => x.InventoryDetails)
                .ThenInclude(d => d.StationeryItem)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    // =========================
    // GET DETAIL
    // =========================
    public async Task<InventoryRecord?> GetByIdAsync(int id)
    {
        return await _context.InventoryRecords
            .Include(x => x.InventoryDetails)
                .ThenInclude(d => d.StationeryItem)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}