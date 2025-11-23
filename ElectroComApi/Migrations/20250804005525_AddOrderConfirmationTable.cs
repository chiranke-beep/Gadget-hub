using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroComApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderConfirmationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "QuotationResponses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "QuotationRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderConfirmations");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "OrderConfirmations");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "OrderConfirmations");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "OrderConfirmations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderConfirmations");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "QuotationResponses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "QuotationRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "OrderConfirmations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
