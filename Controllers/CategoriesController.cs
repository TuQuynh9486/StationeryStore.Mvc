using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;

namespace StationeryStore.Mvc.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return View(categories);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var category =
            await _categoryService.GetDetailAsync(id);

        return View(category);
    }
}