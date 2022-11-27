using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Inventory
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.CreateTable(
                name: "Storages",
                schema: "inventory",
                columns: table => new
                {
                    StorageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ManagerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.StorageId);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                schema: "inventory",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsBase = table.Column<bool>(type: "bit", nullable: false),
                    Ratio = table.Column<int>(type: "int", nullable: false),
                    IsFractional = table.Column<bool>(type: "bit", nullable: false),
                    BaseOfUnitOfMId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.UnitOfMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "inventory",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MainUnitOfMeasureUnitOfMeasureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_UnitOfMeasures_MainUnitOfMeasureUnitOfMeasureId",
                        column: x => x.MainUnitOfMeasureUnitOfMeasureId,
                        principalSchema: "inventory",
                        principalTable: "UnitOfMeasures",
                        principalColumn: "UnitOfMeasureId");
                });

            migrationBuilder.CreateTable(
                name: "ProductsInStorages",
                schema: "inventory",
                columns: table => new
                {
                    StorageId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitOfMeasureId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInStorages", x => new { x.StorageId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsInStorages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "inventory",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInStorages_Storages_StorageId",
                        column: x => x.StorageId,
                        principalSchema: "inventory",
                        principalTable: "Storages",
                        principalColumn: "StorageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInStorages_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "inventory",
                        principalTable: "UnitOfMeasures",
                        principalColumn: "UnitOfMeasureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainUnitOfMeasureUnitOfMeasureId",
                schema: "inventory",
                table: "Products",
                column: "MainUnitOfMeasureUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInStorages_ProductId",
                schema: "inventory",
                table: "ProductsInStorages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInStorages_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductsInStorages",
                column: "UnitOfMeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsInStorages",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Storages",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures",
                schema: "inventory");
        }
    }
}
