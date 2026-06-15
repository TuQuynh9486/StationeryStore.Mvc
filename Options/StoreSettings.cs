namespace StationeryStore.Mvc.Options;

public class StoreSettings
{
    public string StoreName { get; set; } = string.Empty;

    public string SupportEmail { get; set; } = string.Empty;

    public bool EnableSeedData { get; set; }

    public int LowStockThreshold { get; set; }

}