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
    // GET ALL INCLUDING DELETED
    // =========================

    public async Task<List<StationeryItem>>
        GetAllIncludingDeletedAsync()
    {
        return await _context.StationeryItems
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
    }
    // =========================
    // ADD NEW ITEM
    // =========================
    public async Task AddAsync(StationeryItem item)
    {
        await _context.StationeryItems.AddAsync(item);
    }

    // =========================
    // SEARCH ITEM
    // =========================

    public async Task<List<StationeryItem>> SearchAsync(
    int? categoryId,
    decimal? minPrice,
    decimal? maxPrice,
    string? keyword)
    {
        var query = _context.StationeryItems
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.Name.Contains(keyword) ||
                x.Code.Contains(keyword));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x =>
                x.CategoryId == categoryId.Value);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }

    // =========================
    // UPDATE ITEM
    // =========================
    public Task UpdateAsync(StationeryItem item)
    {
        _context.StationeryItems.Update(item);
        return Task.CompletedTask;
    }
    // =========================
    // DELETE ITEM
    // =========================
    public async Task DeleteAsync(int id)
    {
        var item =
            await _context.StationeryItems.FindAsync(id);

        if (item != null)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.UtcNow;
        }
    }

    // =========================
    // SAVE CHANGES 
    // =========================
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // =========================
    // TRASH 
    // =========================
    public async Task<List<StationeryItem>> GetTrashAsync()
    {
        return await _context.StationeryItems
            .IgnoreQueryFilters()
            .Where(x => x.IsDeleted)
            .AsNoTracking()
            .ToListAsync();
    }

    // =========================
    // RESTORE 
    // =========================
    public async Task RestoreAsync(int id)
    {
        var item = await _context.StationeryItems
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item != null)
        {
            item.IsDeleted = false;
            item.DeletedAt = null;
            item.UpdatedAt = DateTime.UtcNow;
        }
    }

    public async Task<bool> ExistsCodeAsync(string code)
    {
        return await _context.StationeryItems
            .AnyAsync(x => x.Code == code);
    }

    public async Task<bool> ExistsCodeExceptIdAsync(
    string code,
    int id)
    {
        return await _context.StationeryItems
            .AnyAsync(x =>
                x.Code == code &&
                x.Id != id);
    }

    public async Task RestoreAsync(
    int id,
    byte[] rowVersion)
    {
        var item =
            await _context.StationeryItems
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return;

        _context.Entry(item)
            .Property(x => x.RowVersion)
            .OriginalValue = rowVersion;

        item.IsDeleted = false;
        item.DeletedAt = null;
    }
}