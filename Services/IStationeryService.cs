using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public interface IStationeryService
{
    Task<List<StationeryListItemViewModel>>
        GetStationeryListAsync();
    Task<StationeryDetailViewModel?>
        GetDetailAsync(int id);
    Task CreateAsync(StationeryCreateViewModel model);

    Task<List<StationeryListItemViewModel>> SearchAsync(StationerySearchViewModel model);

    Task<StationeryStatsViewModel> GetStatsAsync();

    Task<StationeryEditViewModel?> GetEditAsync(int id);

    Task UpdateAsync(StationeryEditViewModel model);

    Task DeleteAsync(int id);

    Task<List<StationeryListItemViewModel>>
    GetTrashAsync();

    Task RestoreAsync(
        int id,
        byte[]? rowVersion);

    Task<AdjustStockViewModel?> GetAdjustStockAsync(int id);

    Task AdjustStockAsync(AdjustStockViewModel model);
}