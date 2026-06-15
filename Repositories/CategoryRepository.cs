using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly StationeryDbContext _context;

    public CategoryRepository(StationeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.StationeryItems)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(x => x.StationeryItems)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}