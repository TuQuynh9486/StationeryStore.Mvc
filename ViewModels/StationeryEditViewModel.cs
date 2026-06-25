using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace StationeryStore.Mvc.ViewModels;

public class StationeryEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = "";

    [Required]
    public int CategoryId { get; set; }


    public IEnumerable<SelectListItem> Categories
    = new List<SelectListItem>();


    [Required]
    public string ImageUrl { get; set; } = "";

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = "";

    [Range(1000, 10000000)]
    public decimal Price { get; set; }

    [Range(0, 10000)]
    public int StockQuantity { get; set; }

    [Range(0, 10000)]
    public int MinStock { get; set; }

    public byte[]? RowVersion { get; set; }
}