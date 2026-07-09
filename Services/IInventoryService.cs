using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public interface IInventoryService
{
    // Hiển thị toàn bộ lịch sử giao dịch
    Task<List<InventoryRecord>> GetAllAsync();

    // Xem chi tiết một giao dịch
    Task<InventoryRecord?> GetByIdAsync(int id);

    // Lịch sử 
    Task<List<InventoryRecord>> GetInventoryHistoryAsync();

    // Tạo giao dịch nhập kho
    Task CreateInventoryRecordAsync(
        int stationeryItemId,
        int quantity,
        string note);
}