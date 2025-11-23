using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFullyComparedResponseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "FullyComparedResponses");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "FullyComparedResponses",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "DistributorName",
                table: "FullyComparedResponses",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FullyComparedResponses");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FullyComparedResponses",
                newName: "DistributorName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "FullyComparedResponses",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "FullyComparedResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "FullyComparedResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
