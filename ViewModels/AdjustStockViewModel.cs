using System.ComponentModel.DataAnnotations;

namespace StationeryStore.Mvc.ViewModels;

public class AdjustStockViewModel
{
    public int Id { get; set; }

    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public int CurrentStock { get; set; }

    [Required]
    public int ChangeQuantity { get; set; }

    public int NewStock =>
    CurrentStock + ChangeQuantity;

    public byte[]? RowVersion { get; set; }
}