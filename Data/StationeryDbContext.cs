using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using StationeryStore.Mvc.Models;

namespace StationeryStore.Mvc.Data;

public class StationeryDbContext : DbContext
{
  public StationeryDbContext(DbContextOptions<StationeryDbContext> options)
      : base(options)
  {
  }

  // =========================
  // DBSets
  // =========================
  public DbSet<StationeryItem> StationeryItems => Set<StationeryItem>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Supplier> Suppliers => Set<Supplier>();
  public DbSet<InventoryRecord> InventoryRecords => Set<InventoryRecord>();
  public DbSet<InventoryDetail> InventoryDetails => Set<InventoryDetail>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // =========================
    // CATEGORY
    // =========================
    modelBuilder.Entity<Category>(entity =>
    {
      entity.ToTable("Categories");

      entity.HasKey(c => c.Id);

      entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
    });

    // =========================
    // SUPPLIER
    // =========================
    modelBuilder.Entity<Supplier>(entity =>
    {
      entity.ToTable("Suppliers");

      entity.HasKey(s => s.Id);

      entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

      entity.Property(s => s.Phone)
                .HasMaxLength(20);
    });

    // =========================
    // STATIONERY ITEM
    // =========================
    modelBuilder.Entity<StationeryItem>(entity =>
    {
      entity.ToTable("StationeryItems");

      entity.HasKey(i => i.Id);

      entity.Property(i => i.Code)
                .IsRequired()
                .HasMaxLength(50);

      entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

      entity.Property(i => i.Brand)
                .HasMaxLength(100);

      entity.Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

      entity.Property(i => i.Description)
                .HasMaxLength(500);

      entity.Property(i => i.ImageUrl)
                .HasMaxLength(255);

      entity.Property(i => i.LastUpdatedAt)
                .IsRequired();

      // Relationship: Category
      entity.HasOne(i => i.Category)
                .WithMany(c => c.StationeryItems)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

      // Relationship: Supplier
      entity.HasOne(i => i.Supplier)
                .WithMany(s => s.StationeryItems)
                .HasForeignKey(i => i.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
    });

    // =========================
    // INVENTORY RECORD
    // =========================
    modelBuilder.Entity<InventoryRecord>(entity =>
    {
      entity.ToTable("InventoryRecords");

      entity.HasKey(r => r.Id);

      entity.Property(r => r.CreatedAt)
                .IsRequired();
    });

    // =========================
    // INVENTORY DETAIL
    // =========================
    modelBuilder.Entity<InventoryDetail>(entity =>
    {
      entity.ToTable("InventoryDetails");

      entity.HasKey(d => d.Id);

      entity.HasOne(d => d.InventoryRecord)
                .WithMany(r => r.InventoryDetails)
                .HasForeignKey(d => d.InventoryRecordId)
                .OnDelete(DeleteBehavior.Cascade);

      entity.HasOne(d => d.StationeryItem)
                .WithMany()
                .HasForeignKey(d => d.StationeryItemId)
                .OnDelete(DeleteBehavior.Restrict);
    });

    // =========================
    // SEED DATA - CATEGORY
    // =========================
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Bút viết" },
        new Category { Id = 2, Name = "Sổ - vở" },
        new Category { Id = 3, Name = "Dụng cụ học tập" }
    );

    // =========================
    // SEED DATA - SUPPLIER
    // =========================
    modelBuilder.Entity<Supplier>().HasData(
        new Supplier { Id = 1, Name = "Thiên Long", Phone = "0901234567" },
        new Supplier { Id = 2, Name = "Hồng Hà", Phone = "0907654321" },
        new Supplier { Id = 3, Name = "FlexOffice", Phone = "0901122334"},
        new Supplier { Id = 4, Name = "Deli", Phone = "0901567234"}
    );

    // =========================
    // SEED DATA - STATIONERY ITEM
    // =========================
    modelBuilder.Entity<StationeryItem>().HasData(
        new StationeryItem
        {
          Id = 1,
          Code = "SP001",
          Name = "Bút bi Thiên Long",
          Brand = "Thiên Long",
          Price = 5000,
          StockQuantity = 100,
          MinStock = 20,
          ImageUrl = "/images/pen.jpg",
          Description = "Bút bi màu xanh, viết trơn.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 1,
          SupplierId = 1
        },
        new StationeryItem
        {
          Id = 2,
          Code = "SP002",
          Name = "Sổ tay Hồng Hà",
          Brand = "Hồng Hà",
          Price = 25000,
          StockQuantity = 50,
          MinStock = 10,
          ImageUrl = "/images/notebook.jpg",
          Description = "Sổ tay 200 trang.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 2,
          SupplierId = 2
        },
        new StationeryItem
        {
          Id = 3,
          Code = "SP003",
          Name = "Thước kẻ 30cm",
          Brand = "FlexOffice",
          Price = 8000,
          StockQuantity = 70,
          MinStock = 15,
          ImageUrl = "/images/ruler.jpg",
          Description = "Thước nhựa trong suốt 30cm.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 3,
          SupplierId = 3
        },
        new StationeryItem
        {
          Id = 4,
          Code = "SP004",
          Name = "Hộp bút vải",
          Brand = "Deli",
          Price = 55000,
          StockQuantity = 20,
          MinStock = 30,
          ImageUrl = "/images/pencilbox.jpg",
          Description = "Hộp bút vải dùng cho học sinh.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 3,
          SupplierId = 4
        },

        new StationeryItem
        {
          Id = 5,
          Code = "SP005",
          Name = "Gôm tẩy học sinh",
          Brand = "FlexOffice",
          Price = 3000,
          StockQuantity = 150,
          MinStock = 40,
          ImageUrl = "/images/eraser.jpg",
          Description = "Gôm tẩy mềm, không làm rách giấy.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 3,
          SupplierId = 3
        },

        new StationeryItem
        {
          Id = 6,
          Code = "SP006",
          Name = "Tập học sinh 200 trang",
          Brand = "Hồng Hà",
          Price = 35000,
          StockQuantity = 60,
          MinStock = 15,
          ImageUrl = "/images/notebook200.jpg",
          Description = "Tập học sinh 200 trang.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 2,
          SupplierId = 2
        },
        
        new StationeryItem
        {
          Id = 7,
          Code = "SP007",
          Name = "Bút chì màu Deli",
          Brand = "Deli",
          Price = 65000,
          StockQuantity = 5,
          MinStock = 15,
          ImageUrl = "/images/pencilcolor.jpg",
          Description = "Bút chì màu 24 cây.",
          LastUpdatedAt = new DateTime(2026, 1, 1),
          CategoryId = 1,
          SupplierId = 4
        }
    );
  }
}