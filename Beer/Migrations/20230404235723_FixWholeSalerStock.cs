using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerVendor.Migrations
{
    /// <inheritdoc />
    public partial class FixWholeSalerStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "WholesalerStocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WholesalerStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 0);
        }
    }
}
