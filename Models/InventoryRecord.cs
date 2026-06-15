namespace StationeryStore.Mvc.Models;

public class InventoryRecord
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
        = DateTime.Now;

    public string Note { get; set; }
        = string.Empty;

    public ICollection<InventoryDetail> InventoryDetails
    {
        get;
        set;
    } = new List<InventoryDetail>();
}