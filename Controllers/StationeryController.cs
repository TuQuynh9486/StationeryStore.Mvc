using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;


namespace StationeryStore.Mvc.Controllers;

public class StationeryController : Controller
{
    private readonly IStationeryService _stationeryService;

    private readonly ICategoryService _categoryService;

    public StationeryController(
    IStationeryService stationeryService,
    ICategoryService categoryService)
    {
        _stationeryService = stationeryService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var items =
            await _stationeryService
                .GetStationeryListAsync();

        return View(items);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new StationeryCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StationeryCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _stationeryService.CreateAsync(model);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Search(
       StationerySearchViewModel model)
    {
        model.Products =
            await _stationeryService.SearchAsync(model);

        model.Categories =
           await _categoryService.GetCategoryListAsync();

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Stats()
    {
        var model =
            await _stationeryService.GetStatsAsync();

        return View(model);
    }

}