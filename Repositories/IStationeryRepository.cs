using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Repositories;

public interface IStationeryRepository
{
    Task<List<StationeryItem>> GetAllAsync();

    Task<List<StationeryItem>> GetAllReadOnlyAsync();
    Task<List<StationeryItem>> GetAllIncludingDeletedAsync();

    Task<StationeryItem?> GetByIdAsync(int id);

    Task UpdateAsync(StationeryItem item);

    Task DeleteAsync(int id);

    Task<List<StationeryItem>> SearchAsync(
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice,
        string? keyword);

    Task AddAsync(StationeryItem item);

    Task SaveChangesAsync();

    Task<List<StationeryItem>> GetTrashAsync();

    Task RestoreAsync(int id);

    Task<bool> ExistsCodeAsync(string code);

    Task<bool> ExistsCodeExceptIdAsync(
        string code,
        int id);

    Task RestoreAsync(
        int id,
        byte[] rowVersion);
}