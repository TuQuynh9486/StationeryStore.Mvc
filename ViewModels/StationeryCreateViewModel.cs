using System.ComponentModel.DataAnnotations;

namespace StationeryStore.Mvc.ViewModels;

public class StationeryCreateViewModel
{
    [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
    [StringLength(50, ErrorMessage = "Mã sản phẩm tối đa 50 ký tự")]
    public string Code { get; set; } = "";

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Thương hiệu không được để trống")]
    [StringLength(100, ErrorMessage = "Thương hiệu tối đa 100 ký tự")]
    public string Brand { get; set; } = "";

    [Required(ErrorMessage = "Danh mục sản phẩm không được để trống")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Nhà cung cấp không được để trống")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Hình ảnh sản phẩm không được để trống")]
    [StringLength(255)]
    public string ImageUrl { get; set; } = "";

    [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
    [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
    public string Description { get; set; } = "";

    [Range(1000, 10000000, ErrorMessage = "Giá bán phải từ 1.000 đến 10.000.000")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Số lượng tồn kho phải từ 0 đến 10.000")]
    public int StockQuantity { get; set; }

    [Range(0, 10000, ErrorMessage = "Mức tồn tối thiểu phải từ 0 đến 10.000")]
    public int MinStock { get; set; }
}