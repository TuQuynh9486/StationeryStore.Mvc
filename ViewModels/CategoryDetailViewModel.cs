namespace StationeryStore.Mvc.ViewModels;

public class CategoryDetailViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public List<StationeryListItemViewModel> Products
        { get; set; } = new();
}