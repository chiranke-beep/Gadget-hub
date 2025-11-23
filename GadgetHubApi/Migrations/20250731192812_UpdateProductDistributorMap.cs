using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDistributorMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ProductDistributorMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GadgetHubId = table.Column<int>(type: "int", nullable: false),
                    TechWorldId = table.Column<int>(type: "int", nullable: true),
                    GadgetCentralId = table.Column<int>(type: "int", nullable: true),
                    ElectroComId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDistributorMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDistributorMaps_Products_GadgetHubId",
                        column: x => x.GadgetHubId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinalResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DistributorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalResponses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FullyComparedResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DistributorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullyComparedResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullyComparedResponses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "Category", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Logitech", "Peripherals", "Ergonomic wireless mouse with adjustable DPI.", "Wireless Mouse", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8" },
                    { 2, "Keychron", "Peripherals", "RGB backlit mechanical keyboard with blue switches.", "Mechanical Keyboard", "https://images.unsplash.com/photo-1519389950473-47ba0277781c" },
                    { 3, "Sony", "Accessories", "Over-ear headphones with active noise cancellation.", "Noise Cancelling Headphones", "https://images.unsplash.com/photo-1511367461989-f85a21fda167" },
                    { 4, "Dell", "Monitors", "27-inch 4K UHD monitor with HDR support.", "4K Monitor", "https://images.unsplash.com/photo-1519125323398-675f0ddb6308" },
                    { 5, "Anker", "Accessories", "Multiport USB-C hub with HDMI, USB 3.0, and SD card reader.", "USB-C Hub", "https://images.unsplash.com/photo-1465101046530-73398c7f28ca" },
                    { 6, "Samsung", "Storage", "1TB portable SSD with USB 3.2 Gen 2 support.", "Portable SSD", "https://images.unsplash.com/photo-1518770660439-4636190af475" },
                    { 7, "Apple", "Wearables", "Fitness tracking smartwatch with heart rate monitor.", "Smartwatch", "https://images.unsplash.com/photo-1516574187841-cb9cc2ca948b" },
                    { 8, "JBL", "Audio", "Waterproof Bluetooth speaker with 12-hour battery life.", "Bluetooth Speaker", "https://images.unsplash.com/photo-1509395176047-4a66953fd231" },
                    { 9, "Logitech", "Peripherals", "1080p HD webcam with built-in microphone.", "Webcam", "https://images.unsplash.com/photo-1515378791036-0648a3ef77b2" },
                    { 10, "Belkin", "Accessories", "Fast wireless charging pad for smartphones.", "Wireless Charger", "https://images.unsplash.com/photo-1512446733611-9099a758e082" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalResponses_OrderId",
                table: "FinalResponses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FullyComparedResponses_OrderId",
                table: "FullyComparedResponses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDistributorMaps_GadgetHubId",
                table: "ProductDistributorMaps",
                column: "GadgetHubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalResponses");

            migrationBuilder.DropTable(
                name: "FullyComparedResponses");

            migrationBuilder.DropTable(
                name: "ProductDistributorMaps");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
