using System.Threading.Tasks;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Services;

public interface IAuditLogService
{
    Task LogAsync(
        string action,
        string entity,
        int? entityId,
        string result,
        string? detail = null);
    
    Task<List<AuditLog>> GetAllAsync();
}