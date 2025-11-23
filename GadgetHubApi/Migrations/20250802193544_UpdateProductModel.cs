using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValue: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductDistributorMaps",
                columns: new[] { "Id", "ElectroComId", "GadgetCentralId", "GadgetHubId", "TechWorldId" },
                values: new object[,]
                {
                    { 1, 301, 201, 1, 101 },
                    { 2, 302, 202, 2, 102 },
                    { 3, 303, 203, 3, 103 },
                    { 4, 304, 204, 4, 104 },
                    { 5, 305, 205, 5, 105 },
                    { 6, 306, 206, 6, 106 },
                    { 7, 307, 207, 7, 107 },
                    { 8, 308, 208, 8, 108 },
                    { 9, 309, 209, 9, 109 },
                    { 10, 310, 210, 10, 110 }
                });
        }
    }
}
