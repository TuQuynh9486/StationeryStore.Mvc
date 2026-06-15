using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();

    Task<Supplier?> GetByIdAsync(int id);

    Task AddAsync(Supplier supplier);

    Task SaveChangesAsync();
}