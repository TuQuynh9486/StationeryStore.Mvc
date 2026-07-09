namespace StationeryStore.Mvc.ViewModels;

public class DashboardViewModel
{
    public int TotalProducts { get; set; }

    public int TotalInventoryTransactions { get; set; }

    public int TotalAuditLogs { get; set; }

    public int TotalCategories { get; set; }

    public int TotalSuppliers { get; set; }

    public bool IdentityEnabled { get; set; }

    public bool AuthorizationEnabled { get; set; }

    public bool AntiForgeryEnabled { get; set; }

    public bool HealthCheckEnabled { get; set; }
}