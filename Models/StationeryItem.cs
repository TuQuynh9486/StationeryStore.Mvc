namespace StationeryStore.Mvc.Models;

public class StationeryItem
{
    // =========================
    // PRIMARY KEY
    // =========================
    public int Id { get; set; }

    // =========================
    // BASIC INFO
    // =========================
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int MinStock { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // =========================
    // AUDIT FIELD
    // =========================
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    // =========================
    // FOREIGN KEYS
    // =========================
    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    // =========================
    // NAVIGATION PROPERTIES
    // =========================
    public Category? Category { get; set; }

    public Supplier? Supplier { get; set; }
}