

namespace StationeryStore.Mvc.ViewModels;

public class StationerySearchViewModel
{
    public string? Keyword { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? CategoryId { get; set; }

    public List<StationeryListItemViewModel> Products
    { get; set; } = new();

    public List<CategoryListItemViewModel> Categories
    { get; set; } = new();
}