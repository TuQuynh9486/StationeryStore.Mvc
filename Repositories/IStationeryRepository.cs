using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public interface IStationeryRepository
{
    Task<List<StationeryItem>> GetAllAsync();

    Task<List<StationeryItem>> GetAllReadOnlyAsync();

    Task<StationeryItem?> GetByIdAsync(int id);

    Task<List<StationeryItem>> SearchAsync(
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice,
        string? keyword);

    Task AddAsync(StationeryItem item);

    Task SaveChangesAsync();
}