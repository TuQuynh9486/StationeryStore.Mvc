using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Repositories;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CategoryDetailViewModel?>
    GetDetailAsync(int id)
    {
        var category =
            await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }


        return new CategoryDetailViewModel
        {
            Id = category.Id,
            Name = category.Name,

            Products = category.StationeryItems
            .Select(x => new StationeryListItemViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Brand = x.Brand,
                Price = x.Price,
                StockQuantity = x.StockQuantity
            })
            .ToList()
        };
    }
}