using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StationeryStore.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StationeryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    MinStock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationeryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationeryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StationeryItems_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryRecordId = table.Column<int>(type: "int", nullable: false),
                    StationeryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_InventoryRecords_InventoryRecordId",
                        column: x => x.InventoryRecordId,
                        principalTable: "InventoryRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryDetails_StationeryItems_StationeryItemId",
                        column: x => x.StationeryItemId,
                        principalTable: "StationeryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bút viết" },
                    { 2, "Sổ - vở" },
                    { 3, "Dụng cụ học tập" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Thiên Long", "0901234567" },
                    { 2, "Hồng Hà", "0907654321" },
                    { 3, "FlexOffice", "0901122334" },
                    { 4, "Deli", "0901567234" }
                });

            migrationBuilder.InsertData(
                table: "StationeryItems",
                columns: new[] { "Id", "Brand", "CategoryId", "Code", "Description", "ImageUrl", "LastUpdatedAt", "MinStock", "Name", "Price", "StockQuantity", "SupplierId" },
                values: new object[,]
                {
                    { 1, "Thiên Long", 1, "SP001", "Bút bi màu xanh, viết trơn.", "/images/pen.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Bút bi Thiên Long", 5000m, 100, 1 },
                    { 2, "Hồng Hà", 2, "SP002", "Sổ tay 200 trang.", "/images/notebook.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Sổ tay Hồng Hà", 25000m, 50, 2 },
                    { 3, "FlexOffice", 3, "SP003", "Thước nhựa trong suốt 30cm.", "/images/ruler.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Thước kẻ 30cm", 8000m, 70, 3 },
                    { 4, "Deli", 3, "SP004", "Hộp bút vải dùng cho học sinh.", "/images/pencilbox.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Hộp bút vải", 55000m, 20, 4 },
                    { 5, "FlexOffice", 3, "SP005", "Gôm tẩy mềm, không làm rách giấy.", "/images/eraser.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "Gôm tẩy học sinh", 3000m, 150, 3 },
                    { 6, "Hồng Hà", 2, "SP006", "Tập học sinh 200 trang.", "/images/notebook200.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Tập học sinh 200 trang", 35000m, 60, 2 },
                    { 7, "Deli", 1, "SP007", "Bút chì màu 24 cây.", "/images/pencilcolor.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Bút chì màu Deli", 65000m, 5, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_InventoryRecordId",
                table: "InventoryDetails",
                column: "InventoryRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDetails_StationeryItemId",
                table: "InventoryDetails",
                column: "StationeryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StationeryItems_CategoryId",
                table: "StationeryItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StationeryItems_SupplierId",
                table: "StationeryItems",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryDetails");

            migrationBuilder.DropTable(
                name: "InventoryRecords");

            migrationBuilder.DropTable(
                name: "StationeryItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
