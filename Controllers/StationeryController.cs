using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Mvc.Controllers;

public class StationeryController : Controller
{
    private readonly StationeryService _stationeryService;

    public StationeryController(
        StationeryService stationeryService)
    {
        _stationeryService = stationeryService;
    }

    public IActionResult Index()
    {
        var items = _stationeryService.GetAll()
            .Select(ToListItemViewModel)
            .ToList();

        return View(items);
    }

    public IActionResult Detail(int id)
    {
        var item = _stationeryService.GetById(id);

        if (item == null)
        {
            return NotFound(
                $"Không tìm thấy sản phẩm có id = {id}");
        }

        var viewModel = ToDetailViewModel(item);

        return View(viewModel);
    }

    public IActionResult Stats()
    {
        var stats = _stationeryService.GetStats();

        return View(stats);
    }

    [HttpGet]
    public IActionResult Search(
        string? keyword,
        decimal? minPrice,
        decimal? maxPrice,
        string? category)
    {
        var items = _stationeryService.Search(
                keyword,
                minPrice,
                maxPrice,
                category)
            .Select(ToListItemViewModel)
            .ToList();

        var viewModel = new StationerySearchViewModel
        {
            Keyword = keyword ?? "",

            MinPrice = minPrice,

            MaxPrice = maxPrice,

            Category = category ?? "",

            Products = items
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new StationeryCreateViewModel
        {
            StockQuantity = 1,

            MinStock = 1
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        StationeryCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _stationeryService.Create(model);

        TempData["SuccessMessage"] =
            "Đã thêm sản phẩm văn phòng phẩm thành công.";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Welcome()
    {
        return Content(
            "Welcome to Mini Stationery Store Catalog MVC");
    }

    public IActionResult StationeryJson()
    {
        var items = _stationeryService.GetAll()
            .Select(item => new
            {
                item.Id,
                item.Code,
                item.Name,
                item.Category,
                item.Brand,
                item.Price,
                item.StockQuantity
            });

        return Json(items);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound(
            "Đây là response 404 demo từ action Force404.");
    }

    private static StationeryListItemViewModel
        ToListItemViewModel(StationeryItem item)
    {
        return new StationeryListItemViewModel
        {
            Id = item.Id,

            Code = item.Code,

            Name = item.Name,

            Category = item.Category,

            Brand = item.Brand,

            Price = item.Price,

            StockQuantity = item.StockQuantity,

            MinStock = item.MinStock,

            ImageUrl = item.ImageUrl
        };
    }

    private static StationeryDetailViewModel
        ToDetailViewModel(StationeryItem item)
    {
        return new StationeryDetailViewModel
        {
            Id = item.Id,

            Code = item.Code,

            Name = item.Name,

            Category = item.Category,

            Brand = item.Brand,

            Price = item.Price,

            StockQuantity = item.StockQuantity,

            MinStock = item.MinStock,

            ImageUrl = item.ImageUrl,

            Description = item.Description,

            LastUpdatedAt = item.LastUpdatedAt
        };
    }
}