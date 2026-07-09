using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly StationeryDbContext _context;

    public InventoryRepository(
        StationeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryRecord>> GetAllAsync()
    {
        return await _context.InventoryRecords
            .Include(i => i.InventoryDetails)
            .AsNoTracking()
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<InventoryRecord?> GetByIdAsync(int id)
    {
        return await _context.InventoryRecords
            .Include(i => i.InventoryDetails)
                .ThenInclude(d => d.StationeryItem)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddAsync(
        InventoryRecord record)
    {
        await _context.InventoryRecords.AddAsync(record);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}