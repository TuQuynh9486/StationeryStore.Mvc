using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public interface ISupplierService
{
    Task<List<SupplierListItemViewModel>>
        GetSupplierListAsync();

    Task CreateAsync(
        SupplierCreateViewModel model);
}