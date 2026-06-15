using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public interface IStationeryService
{
    Task<List<StationeryListItemViewModel>>
        GetStationeryListAsync();
    Task CreateAsync(StationeryCreateViewModel model);

    Task<List<StationeryListItemViewModel>> SearchAsync(StationerySearchViewModel model);

    Task<StationeryStatsViewModel> GetStatsAsync();
}