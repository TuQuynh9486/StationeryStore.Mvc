using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}