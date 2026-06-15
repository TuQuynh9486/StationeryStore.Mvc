using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.ViewModels;
using StationeryStore.Mvc.Services;

namespace StationeryStore.Mvc.Controllers;

public class HealthController : Controller
{
    private readonly IHealthService _healthService;

    public HealthController(
        IHealthService healthService)
    {
        _healthService = healthService;
    }

    public async Task<IActionResult> Index()
    {
        var checks =
            await _healthService.GetHealthChecksAsync();

        return View(checks);
    }
}