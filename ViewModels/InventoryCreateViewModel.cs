namespace StationeryStore.Mvc.ViewModels;
public class InventoryCreateViewModel
{
    public int StationeryItemId { get; set; }

    public int Quantity { get; set; }

    public string Note { get; set; } = "";
}