using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerVendor.Migrations
{
    /// <inheritdoc />
    public partial class AddWholesaler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Vendors_VendorId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "Beers");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "Beers",
                newName: "BreweryId");

            migrationBuilder.RenameIndex(
                name: "IX_Beers_VendorId",
                table: "Beers",
                newName: "IX_Beers_BreweryId");

            migrationBuilder.AlterColumn<double>(
                name: "AlcoholContent",
                table: "Beers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "BreweryId1",
                table: "Beers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Beers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Breweries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wholesalers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WholesalerStocks",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    WholesalerId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WholesalerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalerStocks", x => new { x.WholesalerId, x.BeerId });
                    table.ForeignKey(
                        name: "FK_WholesalerStocks_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholesalerStocks_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholesalerStocks_Wholesalers_WholesalerId1",
                        column: x => x.WholesalerId1,
                        principalTable: "Wholesalers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Abbaye de Leffe" });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "GeneDrinks" });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "BreweryId1", "Name", "Price" },
                values: new object[] { 1, 6.5999999999999996, 1, null, "Leffe Blonde", 2.20m });

            migrationBuilder.InsertData(
                table: "WholesalerStocks",
                columns: new[] { "BeerId", "WholesalerId", "Id", "Quantity", "WholesalerId1" },
                values: new object[] { 1, 1, 0, 10, null });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweryId1",
                table: "Beers",
                column: "BreweryId1");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalerStocks_BeerId",
                table: "WholesalerStocks",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalerStocks_WholesalerId1",
                table: "WholesalerStocks",
                column: "WholesalerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Breweries_BreweryId",
                table: "Beers",
                column: "BreweryId",
                principalTable: "Breweries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Breweries_BreweryId1",
                table: "Beers",
                column: "BreweryId1",
                principalTable: "Breweries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Breweries_BreweryId",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Breweries_BreweryId1",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "Breweries");

            migrationBuilder.DropTable(
                name: "WholesalerStocks");

            migrationBuilder.DropTable(
                name: "Wholesalers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BreweryId1",
                table: "Beers");

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "BreweryId1",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Beers");

            migrationBuilder.RenameColumn(
                name: "BreweryId",
                table: "Beers",
                newName: "VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_Beers_BreweryId",
                table: "Beers",
                newName: "IX_Beers_VendorId");

            migrationBuilder.AlterColumn<decimal>(
                name: "AlcoholContent",
                table: "Beers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "Beers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Vendors_VendorId",
                table: "Beers",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
