using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFinalResponseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "FinalResponses");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "FinalResponses",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "DistributorName",
                table: "FinalResponses",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "FinalResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FinalResponses");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FinalResponses",
                newName: "DistributorName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "FinalResponses",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "FinalResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "FinalResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "FinalResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
