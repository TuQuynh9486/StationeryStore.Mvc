using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;
using StationeryStore.Mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace StationeryStore.Mvc.Controllers;

public class StationeryController : Controller
{
    private readonly IStationeryService _stationeryService;

    private readonly ICategoryService _categoryService;

    private readonly ISupplierService _supplierService;

    public StationeryController(
    IStationeryService stationeryService,
    ICategoryService categoryService,
    ISupplierService supplierService)
    {
        _stationeryService = stationeryService;
        _categoryService = categoryService;
        _supplierService = supplierService;
    }

    public async Task<IActionResult> Index()
    {
        var items =
            await _stationeryService
                .GetStationeryListAsync();

        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var item =
            await _stationeryService
                .GetDetailAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
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

        try
        {
            await _stationeryService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                "",
                ex.Message);

            return View(model);
        }
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
    public async Task<IActionResult> Dashboard()
    {
        var model =
            await _stationeryService.GetStatsAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model =
            await _stationeryService.GetEditAsync(id);
        if (model == null)
            return NotFound();

        var categories =
            await _categoryService.GetCategoryListAsync();

        model.Categories =
            categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        StationeryEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _stationeryService.UpdateAsync(model);

            TempData["SuccessMessage"] =
                "Cập nhật sản phẩm thành công";

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            ModelState.AddModelError(
                "",
                "Dữ liệu đã bị thay đổi bởi người dùng khác.");

            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                "",
                ex.Message);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item =
            await _stationeryService.GetDetailAsync(id);

        if (item == null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _stationeryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Trash()
    {
        var items =
            await _stationeryService.GetTrashAsync();

        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restore(
     int id,
     string rowVersion)
    {
        if (string.IsNullOrWhiteSpace(rowVersion))
        {
            return BadRequest();
        }
        var bytes =
            Convert.FromBase64String(rowVersion);

        try
        {
            await _stationeryService
                .RestoreAsync(id, bytes);

            TempData["SuccessMessage"] =
                "Khôi phục thành công";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] =
                ex.Message;
        }

        return RedirectToAction(nameof(Trash));
    }

    [HttpGet]
    public async Task<IActionResult> AdjustStock(int id)
    {
        var model =
            await _stationeryService
                .GetAdjustStockAsync(id);

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdjustStock(
    AdjustStockViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _stationeryService
                .AdjustStockAsync(model);

            TempData["SuccessMessage"] =
                "Điều chỉnh tồn kho thành công";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                "",
                ex.Message);

            return View(model);
        }
    }
}