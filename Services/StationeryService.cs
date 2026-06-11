using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.ViewModels;

namespace StationeryStore.Mvc.Services;

public class StationeryService
{
    private readonly List<StationeryItem> _items = new()
    {
        new StationeryItem
        {
            Id = 1,
            Code = "PEN-001",
            Name = "Bút bi Thiên Long",
            Category = "Bút",
            Brand = "Thiên Long",
            Price = 10000,
            StockQuantity = 50,
            MinStock = 10,
            ImageUrl = "/images/pen.jpg",
            Description = "Bút bi mực xanh viết mượt, phù hợp cho học sinh và nhân viên văn phòng.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        },

        new StationeryItem
        {
            Id = 2,
            Code = "NOTE-002",
            Name = "Sổ tay A5",
            Category = "Sổ tay",
            Brand = "Hồng Hà",
            Price = 35000,
            StockQuantity = 8,
            MinStock = 10,
            ImageUrl = "/images/notebook.jpg",
            Description = "Sổ tay bìa cứng khổ A5 tiện lợi cho ghi chú hằng ngày.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        },

        new StationeryItem
        {
            Id = 3,
            Code = "BOOK-003",
            Name = "Tập học sinh 200 trang",
            Category = "Tập vở",
            Brand = "Campus",
            Price = 22000,
            StockQuantity = 0,
            MinStock = 15,
            ImageUrl = "/images/notebook200.jpg",
            Description = "Tập học sinh giấy trắng đẹp, chống lem mực.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        },

        new StationeryItem
        {
            Id = 4,
            Code = "RUL-004",
            Name = "Thước kẻ 30cm",
            Category = "Thước",
            Brand = "FlexOffice",
            Price = 12000,
            StockQuantity = 20,
            MinStock = 5,
            ImageUrl = "/images/ruler.jpg",
            Description = "Thước nhựa trong suốt, độ bền cao.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        },

        new StationeryItem
        {
            Id = 5,
            Code = "ERS-005",
            Name = "Gôm tẩy học sinh",
            Category = "Gôm tẩy",
            Brand = "Pentel",
            Price = 8000,
            StockQuantity = 3,
            MinStock = 10,
            ImageUrl = "/images/eraser.jpg",
            Description = "Gôm tẩy mềm, sạch và không làm rách giấy.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        },

        new StationeryItem
        {
            Id = 6,
            Code = "BOX-006",
            Name = "Hộp bút vải",
            Category = "Hộp bút",
            Brand = "Deli",
            Price = 55000,
            StockQuantity = 12,
            MinStock = 5,
            ImageUrl = "/images/pencilbox.jpg",
            Description = "Hộp bút nhiều ngăn tiện dụng cho học sinh.",
            LastUpdatedAt = new DateTime(2026, 5, 22, 19, 12, 0)
        }
    };

    public List<StationeryItem> GetAll()
    {
        return _items;
    }

    public StationeryItem? GetById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public List<StationeryItem> Search(
        string? keyword,
        decimal? minPrice,
        decimal? maxPrice,
        string? category)
    {
        var query = _items.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(item =>
                item.Name.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase) ||

                item.Category.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase) ||

                item.Brand.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase) ||

                item.Code.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(item =>
                item.Category.Equals(category,
                    StringComparison.OrdinalIgnoreCase));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(item =>
                item.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(item =>
                item.Price <= maxPrice.Value);
        }

        return query.ToList();
    }

    public StationeryItem Create(
        StationeryCreateViewModel model)
    {
        var newId = _items.Count == 0
            ? 1
            : _items.Max(item => item.Id) + 1;

        var item = new StationeryItem
        {
            Id = newId,

            Code = model.Code,

            Name = model.Name,

            Category = model.Category,

            Brand = model.Brand,

            Price = model.Price,

            StockQuantity = model.StockQuantity,

            MinStock = model.MinStock,

            ImageUrl = model.ImageUrl,

            Description = model.Description,

            LastUpdatedAt = DateTime.Now
        };

        _items.Add(item);

        return item;
    }

    public StationeryStatsViewModel GetStats()
    {
        var totalProducts = _items.Count;

        var totalStockQuantity = _items.Sum(item =>
            item.StockQuantity);

        var totalInventoryValue = _items.Sum(item =>
            item.Price * item.StockQuantity);

        var outOfStockCount = _items.Count(item =>
            item.StockQuantity <= 0);

        var lowStockCount = _items.Count(item =>
            item.StockQuantity > 0 &&
            item.StockQuantity <= item.MinStock);

        return new StationeryStatsViewModel
        {
            TotalProducts = totalProducts,

            TotalStockQuantity = totalStockQuantity,

            TotalInventoryValue = totalInventoryValue,

            OutOfStockCount = outOfStockCount,

            LowStockCount = lowStockCount
        };
    }
}