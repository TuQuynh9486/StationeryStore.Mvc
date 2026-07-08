using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StationeryStore.Mvc.Controllers;

[Authorize]
public class SuppliersController : Controller
{
    private readonly ISupplierService _service;

    public SuppliersController(
        ISupplierService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data =
            await _service.GetSupplierListAsync();

        return View(data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(
            new SupplierCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        SupplierCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _service.CreateAsync(model);

        return RedirectToAction(nameof(Index));
    }
}