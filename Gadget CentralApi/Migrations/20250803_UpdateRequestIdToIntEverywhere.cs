using Microsoft.EntityFrameworkCore.Migrations;

namespace Gadget_CentralApi.Migrations
{
    public partial class UpdateRequestIdToIntEverywhere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // QuotationRequests: Drop and recreate RequestId as int
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "QuotationRequests");
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "QuotationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // QuotationResponses: Drop and recreate RequestId as int
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "QuotationResponses");
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "QuotationResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // OrderConfirmations: Drop and recreate RequestId as int
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "OrderConfirmations");
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "OrderConfirmations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // QuotationRequests: revert to string
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "QuotationRequests");
            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "QuotationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // QuotationResponses: revert to string
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "QuotationResponses");
            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "QuotationResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // OrderConfirmations: revert to string
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "OrderConfirmations");
            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "OrderConfirmations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
