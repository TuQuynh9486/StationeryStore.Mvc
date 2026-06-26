namespace StationeryStore.Mvc.ViewModels;

public class StationeryStatsViewModel
{
    // Tổng số sản phẩm
    public int TotalProducts { get; set; }

    // Đang hoạt động
    public int ActiveProducts { get; set; }

    // Đã xóa mềm
    public int DeletedProducts { get; set; }

    // Tạo hôm nay
    public int CreatedToday { get; set; }

    // Cập nhật hôm nay
    public int UpdatedToday { get; set; }

    // Tổng tồn kho
    public int TotalStockQuantity { get; set; }

    // Tổng giá trị tồn kho
    public decimal TotalInventoryValue { get; set; }

    // Hết hàng
    public int OutOfStockCount { get; set; }

    // Sắp hết
    public int LowStockCount { get; set; }

    public int LowStockThreshold { get; set; }

    public string TotalInventoryValueText
        => $"{TotalInventoryValue:N0} VND";
}