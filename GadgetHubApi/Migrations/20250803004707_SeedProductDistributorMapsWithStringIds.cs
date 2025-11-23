using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductDistributorMapsWithStringIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Clear existing data first - delete all existing mappings
            migrationBuilder.Sql("DELETE FROM ProductDistributorMaps");

            // Insert new data with string IDs
            migrationBuilder.InsertData(
                table: "ProductDistributorMaps",
                columns: new[] { "Id", "GadgetHubId", "TechWorldId", "GadgetCentralId", "ElectroComId" },
                values: new object[,]
                {
                    { 1, 1, "101", "201", "301" },
                    { 2, 2, "102", "202", "302" },
                    { 3, 3, "103", "203", "303" },
                    { 4, 4, "104", "204", "304" },
                    { 5, 5, "105", "205", "305" },
                    { 6, 6, "106", "206", "306" },
                    { 7, 7, "107", "207", "307" },
                    { 8, 8, "108", "208", "308" },
                    { 9, 9, "109", "209", "309" },
                    { 10, 10, "110", "210", "310" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the seeded data
            migrationBuilder.DeleteData(
                table: "ProductDistributorMaps",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }
    }
}
