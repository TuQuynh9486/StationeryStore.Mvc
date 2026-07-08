using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StationeryStore.Mvc.ViewModels;

public class UploadImageViewModel
{
    // =========================
    // Product Information
    // =========================

    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    // =========================
    // Current Image
    // =========================

    public string? CurrentImage { get; set; }

    // =========================
    // Upload New Image
    // =========================

    [Required(ErrorMessage = "Vui lòng chọn một ảnh.")]
    [Display(Name = "Ảnh mới")]
    public IFormFile? ImageFile { get; set; }
}