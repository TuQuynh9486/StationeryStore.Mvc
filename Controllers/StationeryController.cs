using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;


namespace StationeryStore.Mvc.Controllers;

public class StationeryController : Controller
{
    private readonly IStationeryService _stationeryService;

    public StationeryController(
        IStationeryService stationeryService)
    {
        _stationeryService = stationeryService;
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
    public async Task<IActionResult> Search(StationerySearchViewModel model)
    {
        var result = await _stationeryService.SearchAsync(model);

        model.Products = result;

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