using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationeryStore.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditSoftDeleteRowVersio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StationeryItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StationeryItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StationeryItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "StationeryItems",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StationeryItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });

            migrationBuilder.UpdateData(
                table: "StationeryItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "DeletedAt", "IsDeleted", "UpdatedAt" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StationeryItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StationeryItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StationeryItems");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "StationeryItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StationeryItems");
        }
    }
}
