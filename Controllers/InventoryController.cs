using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StationeryStore.Mvc.Controllers;

[Authorize]
public class InventoryController : Controller
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(
        IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new InventoryCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        InventoryCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _inventoryService.CreateInventoryRecordAsync(
            model.StationeryItemId,
            model.Quantity,
            model.Note);

        return RedirectToAction("Index", "Stationery");
    }
}