namespace StationeryStore.Mvc.ViewModels;

public class StationeryDetailViewModel
{
    public int Id { get; set; }

    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    public string Brand { get; set; } = "";

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int MinStock { get; set; }

    public string ImageUrl { get; set; } = "";

    public string Description { get; set; } = "";

    public DateTime LastUpdatedAt { get; set; }

    public string PriceText => $"{Price:N0} VND";

    public decimal InventoryValue => Price * StockQuantity;

    public string InventoryValueText => $"{InventoryValue:N0} VND";

    public string LastUpdatedText => LastUpdatedAt.ToString("dd/MM/yyyy HH:mm");

    public string StockStatus
    {
        get
        {
            if (StockQuantity <= 0)
            {
                return "Hết hàng";
            }

            if (StockQuantity <= MinStock)
            {
                return "Sắp hết hàng";
            }

            return "Còn hàng";
        }
    }

    public string RestockSuggestion
    {
        get
        {
            if (StockQuantity <= 0)
            {
                return "Cần nhập thêm sản phẩm ngay vì hiện đã hết hàng.";
            }

            if (StockQuantity <= MinStock)
            {
                return $"Nên nhập thêm sản phẩm. Tồn kho hiện tại chỉ còn {StockQuantity}, mức tối thiểu là {MinStock}.";
            }

            return "Số lượng tồn kho hiện đang ổn định.";
        }
    }
}