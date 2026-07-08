using System.ComponentModel.DataAnnotations;

namespace StationeryStore.Mvc.Models;

public class AuditLog
{
    public int Id { get; set; }

    // User thực hiện
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = "";

    // Login, Logout, Create, Edit...
    [Required]
    [MaxLength(50)]
    public string Action { get; set; } = "";

    // StationeryItem, Category...
    [Required]
    [MaxLength(100)]
    public string EntityName { get; set; } = "";

    // Id của entity
    public int? EntityId { get; set; }

    // Success / Failed
    [Required]
    [MaxLength(30)]
    public string Result { get; set; } = "";

    // Chi tiết
    [MaxLength(500)]
    public string Description { get; set; } = "";

    // Thời gian
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // TraceId của request
    [MaxLength(100)]
    public string? TraceId { get; set; }

    // Đường dẫn request
    [MaxLength(200)]
    public string? RequestPath { get; set; }

    // IP client 
    [MaxLength(50)]
    public string? IpAddress { get; set; }
}