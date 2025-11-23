using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Ergonomic wireless mouse with adjustable DPI.", "Wireless Mouse", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8" },
                    { 2, "RGB backlit mechanical keyboard with blue switches.", "Mechanical Keyboard", "https://images.unsplash.com/photo-1519389950473-47ba0277781c" },
                    { 3, "Over-ear headphones with active noise cancellation.", "Noise Cancelling Headphones", "https://images.unsplash.com/photo-1511367461989-f85a21fda167" },
                    { 4, "27-inch 4K UHD monitor with HDR support.", "4K Monitor", "https://images.unsplash.com/photo-1519125323398-675f0ddb6308" },
                    { 5, "Multiport USB-C hub with HDMI, USB 3.0, and SD card reader.", "USB-C Hub", "https://images.unsplash.com/photo-1465101046530-73398c7f28ca" },
                    { 6, "1TB portable SSD with USB 3.2 Gen 2 support.", "Portable SSD", "https://images.unsplash.com/photo-1518770660439-4636190af475" },
                    { 7, "Fitness tracking smartwatch with heart rate monitor.", "Smartwatch", "https://images.unsplash.com/photo-1516574187841-cb9cc2ca948b" },
                    { 8, "Waterproof Bluetooth speaker with 12-hour battery life.", "Bluetooth Speaker", "https://images.unsplash.com/photo-1509395176047-4a66953fd231" },
                    { 9, "1080p HD webcam with built-in microphone.", "Webcam", "https://images.unsplash.com/photo-1515378791036-0648a3ef77b2" },
                    { 10, "Fast wireless charging pad for smartphones.", "Wireless Charger", "https://images.unsplash.com/photo-1512446733611-9099a758e082" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);
        }
    }
}
