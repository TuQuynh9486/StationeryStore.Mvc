using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public interface IInventoryService
{
    Task<List<InventoryRecord>>
        GetInventoryHistoryAsync();

    Task CreateInventoryRecordAsync(
        int stationeryItemId,
        int quantity,
        string note);

    
}