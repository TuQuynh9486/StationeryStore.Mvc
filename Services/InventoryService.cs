using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public class InventoryService : IInventoryService
{
    private readonly StationeryDbContext _context;

    public InventoryService(
        StationeryDbContext context)
    {
        _context = context;
    }

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
                CreatedAt = DateTime.Now,
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
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<InventoryRecord>>
        GetInventoryHistoryAsync()
    {
        return await _context.InventoryRecords
            .Include(x => x.InventoryDetails)
            .AsNoTracking()
            .ToListAsync();
    }
}