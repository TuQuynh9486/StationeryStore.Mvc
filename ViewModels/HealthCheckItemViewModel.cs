namespace StationeryStore.Mvc.ViewModels;

public class HealthCheckItemViewModel
{
    public string Check { get; set; } = "";

    public string Expected { get; set; } = "";

    public string Actual { get; set; } = "";

    public string Status { get; set; } = "";

    public string Note { get; set; } = "";
}