using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDistributorResponseAndProductDistributorMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationRequests");

            migrationBuilder.AddColumn<int>(
                name: "ElectroComId",
                table: "ProductDistributorMaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GadgetCentralId",
                table: "ProductDistributorMaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GadgetHubId",
                table: "ProductDistributorMaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechWorldId",
                table: "ProductDistributorMaps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DistributorResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: false),
                    QuotationResponseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributorResponses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributorResponses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorResponses_ProductId",
                table: "DistributorResponses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorResponses_UserId",
                table: "DistributorResponses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ElectroComId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "GadgetCentralId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "GadgetHubId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "TechWorldId",
                table: "ProductDistributorMaps");

            migrationBuilder.CreateTable(
                name: "QuotationRequests",
                columns: table => new
                {
                    QuotationRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.QuotationRequestId);
                });
        }
    }
}
