using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();

    Task<CategoryDetailViewModel?> GetDetailAsync(int id);

    Task<List<CategoryListItemViewModel>>
    GetCategoryListAsync();
}