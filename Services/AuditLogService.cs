using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StationeryStore.Mvc.Data;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public class AuditLogService : IAuditLogService
{
    private readonly StationeryDbContext _context;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ILogger<AuditLogService> _logger;

    public AuditLogService(
        StationeryDbContext context,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        ILogger<AuditLogService> logger)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    //======================================================
    // WRITE AUDIT LOG
    //======================================================

    public async Task LogAsync(
        string action,
        string entity,
        int? entityId,
        string result,
        string? detail = null)
    {
        try
        {
            var principal =
                _httpContextAccessor.HttpContext?.User;

            ApplicationUser? user = null;

            if (principal != null)
            {
                user =
                    await _userManager.GetUserAsync(principal);
            }

            var log = new AuditLog
            {
                UserName = user?.Email ?? "Anonymous",

                Action = action,

                EntityName = entity,

                EntityId = entityId,

                Result = result,

                Description = detail ?? string.Empty,

                CreatedAt = DateTime.UtcNow,

                TraceId =
                    _httpContextAccessor.HttpContext?.TraceIdentifier,

                RequestPath =
                    _httpContextAccessor.HttpContext?
                        .Request.Path.ToString(),

                IpAddress =
                    _httpContextAccessor.HttpContext?
                        .Connection.RemoteIpAddress?
                        .ToString()
            };

            _context.AuditLogs.Add(log);

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "AuditLog saved. Action={Action}, Entity={Entity}, User={User}",
                action,
                entity,
                user?.Email ?? "Anonymous");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Cannot write AuditLog.");
        }
    }

    //======================================================
    // GET ALL AUDIT LOGS
    //======================================================

    public async Task<List<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLogs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}