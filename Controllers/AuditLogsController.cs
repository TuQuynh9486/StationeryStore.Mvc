using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.Services;

namespace StationeryStore.Mvc.Controllers;

[Authorize(Policy = "CanViewAuditLog")]
public class AuditLogsController : Controller
{
    private readonly IAuditLogService _auditService;

    public AuditLogsController(
        IAuditLogService auditService)
    {
        _auditService = auditService;
    }

    public async Task<IActionResult> Index()
    {
        var logs =
            await _auditService.GetAllAsync();

        return View(logs);
    }
}