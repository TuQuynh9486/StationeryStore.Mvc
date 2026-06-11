namespace StationeryStore.Mvc.ViewModels;

public class StationeryStatsViewModel
{
    public int TotalProducts { get; set; }

    public int TotalStockQuantity { get; set; }

    public decimal TotalInventoryValue { get; set; }

    public int OutOfStockCount { get; set; }

    public int LowStockCount { get; set; }

    public string TotalInventoryValueText => $"{TotalInventoryValue:N0} VND";
}