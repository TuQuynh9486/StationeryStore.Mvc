using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public class StationeryRepository : IStationeryRepository
{
    private readonly StationeryDbContext _context;

    public StationeryRepository(StationeryDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL (TRACKING)
    // =========================
    public async Task<List<StationeryItem>> GetAllAsync()
    {
        return await _context.StationeryItems
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .ToListAsync();
    }

    // =========================
    // GET ALL (READ ONLY)
    // =========================
    public async Task<List<StationeryItem>> GetAllReadOnlyAsync()
    {
        return await _context.StationeryItems
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .AsNoTracking()
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<StationeryItem?> GetByIdAsync(int id)
    {
        return await _context.StationeryItems
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // =========================
    // ADD NEW ITEM
    // =========================
    public async Task AddAsync(StationeryItem item)
    {
        await _context.StationeryItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    // =========================
    // UPDATE ITEM
    // =========================
    public async Task UpdateAsync(StationeryItem item)
    {
        _context.StationeryItems.Update(item);
        await _context.SaveChangesAsync();
    }

    // =========================
    // DELETE ITEM
    // =========================
    public async Task DeleteAsync(int id)
    {
        var item = await _context.StationeryItems.FindAsync(id);

        if (item != null)
        {
            _context.StationeryItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // =========================
    // SAVE CHANGES (OPTIONAL - nếu dùng Unit of Work style)
    // =========================
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}