using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repository;

    public SupplierService(
        ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SupplierListItemViewModel>>
        GetSupplierListAsync()
    {
        var suppliers =
            await _repository.GetAllAsync();

        return suppliers.Select(x =>
            new SupplierListItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                ProductCount =
                    x.StationeryItems.Count
            }).ToList();
    }

    public async Task CreateAsync(
        SupplierCreateViewModel model)
    {
        var supplier = new Supplier
        {
            Name = model.Name,
            Phone = model.Phone
        };

        await _repository.AddAsync(supplier);
    }
}