namespace StationeryStore.Mvc.ViewModels;

public class StationeryListItemViewModel
{
    public int Id { get; set; }

    public string Code { get; set; } = "";

    public string Barcode { get; set; } = "";

    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    public string Brand { get; set; } = "";

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int MinStock { get; set; }

    public string ImageUrl { get; set; } = "";

    public string PriceText => $"{Price:N0} VND";

    public decimal InventoryValue => Price * StockQuantity;

    public string InventoryValueText => $"{InventoryValue:N0} VND";

    public string StockStatus { get; set; } = "";

    public string StockStatusClass { get; set; } = "";




}