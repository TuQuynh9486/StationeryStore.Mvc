namespace StationeryStore.Mvc.ViewModels;

public class CategoryListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public int ProductCount { get; set; }

    public string ProductsText { get; set; } = "";

    public string Relationship { get; set; } = "1 - Many";

    public string DbSetName { get; set; } = "Categories";
}