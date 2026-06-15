using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly StationeryDbContext _context;

    public SupplierRepository(
        StationeryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers
            .Include(x => x.StationeryItems)
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers
            .Include(x => x.StationeryItems)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}