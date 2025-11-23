using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses");

            migrationBuilder.DropIndex(
                name: "IX_FullyComparedResponses_OrderId",
                table: "FullyComparedResponses");

            migrationBuilder.DropIndex(
                name: "IX_FinalResponses_OrderId",
                table: "FinalResponses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "FullyComparedResponses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "FinalResponses");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "FullyComparedResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "FinalResponses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FullyComparedResponses_OrderId",
                table: "FullyComparedResponses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalResponses_OrderId",
                table: "FinalResponses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalResponses_Orders_OrderId",
                table: "FinalResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FullyComparedResponses_Orders_OrderId",
                table: "FullyComparedResponses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
