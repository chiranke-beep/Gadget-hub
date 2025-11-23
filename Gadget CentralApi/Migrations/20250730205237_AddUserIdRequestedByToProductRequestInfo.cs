using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gadget_CentralApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdRequestedByToProductRequestInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestedBy",
                table: "ProductRequestInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductRequestInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "ProductRequestInfos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductRequestInfos");
        }
    }
}
