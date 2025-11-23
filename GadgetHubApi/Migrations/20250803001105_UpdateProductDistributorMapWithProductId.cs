using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GadgetHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDistributorMapWithProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductDistributorMaps_ProductId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "DistributorProductId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropColumn(
                name: "GadgetHubId",
                table: "ProductDistributorMaps");

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "DistributorResponses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponseData",
                table: "DistributorResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "DistributorResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "QuotationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDistributorMaps_ProductId",
                table: "ProductDistributorMaps",
                column: "ProductId",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorResponses_QuotationRequests_QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropTable(
                name: "QuotationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProductDistributorMaps_ProductId",
                table: "ProductDistributorMaps");

            migrationBuilder.DropIndex(
                name: "IX_DistributorResponses_QuotationRequestId",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ResponseData",
                table: "DistributorResponses");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "DistributorResponses");

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "ProductDistributorMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DistributorProductId",
                table: "ProductDistributorMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GadgetHubId",
                table: "ProductDistributorMaps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDistributorMaps_ProductId",
                table: "ProductDistributorMaps",
                column: "ProductId");
        }
    }
}
