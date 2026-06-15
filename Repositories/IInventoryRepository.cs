using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public interface IInventoryRepository
{
    Task<List<InventoryRecord>> GetAllAsync();

    Task<InventoryRecord?> GetByIdAsync(int id);

    Task AddAsync(InventoryRecord record);

    Task SaveChangesAsync();
}