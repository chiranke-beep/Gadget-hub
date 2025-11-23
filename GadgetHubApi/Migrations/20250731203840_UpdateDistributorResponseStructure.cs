using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDistributorResponseStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_Orders_OrderId",
                table: "DistributorResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_Products_ProductId",
                table: "DistributorResponses");

            migrationBuilder.DropIndex(
                name: "IX_DistributorResponses_OrderId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "DistributorResponses");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "DistributorResponses",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "DistributorResponses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "DistributorResponses",
                newName: "Stock");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DistributorResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "DistributorResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "DistributorResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorResponses_UserId",
                table: "DistributorResponses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorResponses_Products_ProductId",
                table: "DistributorResponses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorResponses_Users_UserId",
                table: "DistributorResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_Products_ProductId",
                table: "DistributorResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_Users_UserId",
                table: "DistributorResponses");

            migrationBuilder.DropIndex(
                name: "IX_DistributorResponses_UserId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "DistributorResponses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DistributorResponses",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "DistributorResponses",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DistributorResponses",
                newName: "UnitPrice");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "DistributorResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "DistributorResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "DistributorResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "DistributorResponses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorResponses_OrderId",
                table: "DistributorResponses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorResponses_Orders_OrderId",
                table: "DistributorResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorResponses_Products_ProductId",
                table: "DistributorResponses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
