using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuotationRequestFromGadgetHub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_QuotationRequests_QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDistributorMaps_Products_ProductId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropTable(
                name: "QuotationRequests");

            migrationBuilder.DropIndex(
                name: "IX_DistributorResponses_QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "QuotationResponseId",
                table: "DistributorResponses");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductDistributorMaps",
                newName: "GadgetHubId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDistributorMaps_ProductId",
                table: "ProductDistributorMaps",
                newName: "IX_ProductDistributorMaps_GadgetHubId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDistributorMaps_Products_GadgetHubId",
                table: "ProductDistributorMaps",
                column: "GadgetHubId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDistributorMaps_Products_GadgetHubId",
                table: "ProductDistributorMaps");

            migrationBuilder.RenameColumn(
                name: "GadgetHubId",
                table: "ProductDistributorMaps",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDistributorMaps_GadgetHubId",
                table: "ProductDistributorMaps",
                newName: "IX_ProductDistributorMaps_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "QuotationRequestId",
                table: "DistributorResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuotationResponseId",
                table: "DistributorResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "QuotationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistributorResponses_QuotationRequestId",
                table: "DistributorResponses",
                column: "QuotationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorResponses_QuotationRequests_QuotationRequestId",
                table: "DistributorResponses",
                column: "QuotationRequestId",
                principalTable: "QuotationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDistributorMaps_Products_ProductId",
                table: "ProductDistributorMaps",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
