using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeedInventory.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedpropertyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Supplier",
                table: "Seeds",
                newName: "BatchNo");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Seeds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Seeds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Seeds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Seeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Seeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_SupplierId",
                table: "Seeds",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Suppliers_SupplierId",
                table: "Seeds",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Suppliers_SupplierId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_SupplierId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Seeds");

            migrationBuilder.RenameColumn(
                name: "BatchNo",
                table: "Seeds",
                newName: "Supplier");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Seeds",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
