using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationeryStore.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddBarcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "StationeryItems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Barcode",
                value: "893500180001");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Barcode",
                value: "893500180002");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Barcode",
                value: "893500180003");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "Barcode",
                value: "893500180004");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "Barcode",
                value: "893500180005");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "Barcode",
                value: "893500180006");

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "Barcode",
                value: "893500180007");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "StationeryItems");
        }
    }
}
