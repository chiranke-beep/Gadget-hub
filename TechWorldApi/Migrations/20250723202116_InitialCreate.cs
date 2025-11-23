using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWorldApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderConfirmations",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderConfirmations", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "QuotationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderConfirmationId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityConfirmed = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmedProducts_OrderConfirmations_OrderConfirmationId",
                        column: x => x.OrderConfirmationId,
                        principalTable: "OrderConfirmations",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRequestInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRequestInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRequestInfos_QuotationRequests_QuotationRequestId",
                        column: x => x.QuotationRequestId,
                        principalTable: "QuotationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuoteInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationResponseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    EstimatedDeliveryDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuoteInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuoteInfos_QuotationResponses_QuotationResponseId",
                        column: x => x.QuotationResponseId,
                        principalTable: "QuotationResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedProducts_OrderConfirmationId",
                table: "ConfirmedProducts",
                column: "OrderConfirmationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuoteInfos_QuotationResponseId",
                table: "ProductQuoteInfos",
                column: "QuotationResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequestInfos_QuotationRequestId",
                table: "ProductRequestInfos",
                column: "QuotationRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmedProducts");

            migrationBuilder.DropTable(
                name: "ProductQuoteInfos");

            migrationBuilder.DropTable(
                name: "ProductRequestInfos");

            migrationBuilder.DropTable(
                name: "OrderConfirmations");

            migrationBuilder.DropTable(
                name: "QuotationResponses");

            migrationBuilder.DropTable(
                name: "QuotationRequests");
        }
    }
}
