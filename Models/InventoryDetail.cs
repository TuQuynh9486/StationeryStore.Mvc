namespace StationeryStore.Mvc.Models;

public class InventoryDetail
{
    public int Id { get; set; }

    public int InventoryRecordId { get; set; }

    public int StationeryItemId { get; set; }

    public int Quantity { get; set; }

    public InventoryRecord? InventoryRecord { get; set; }

    public StationeryItem? StationeryItem { get; set; }
}