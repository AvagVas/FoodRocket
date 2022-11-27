using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Orders
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orders");

            migrationBuilder.CreateTable(
                name: "Dishes",
                schema: "orders",
                columns: table => new
                {
                    DishId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                schema: "orders",
                columns: table => new
                {
                    IngredientId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "orders",
                columns: table => new
                {
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => new { x.MenuId, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    ManagerId = table.Column<long>(type: "bigint", nullable: false),
                    WaiterId = table.Column<long>(type: "bigint", nullable: false),
                    TotalSum = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "PriceOffers",
                schema: "orders",
                columns: table => new
                {
                    PriceOfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewPrice = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PromotionalText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DishId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOffers", x => x.PriceOfferId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientDish",
                schema: "orders",
                columns: table => new
                {
                    IngredientId = table.Column<long>(type: "bigint", nullable: false),
                    DishId = table.Column<long>(type: "bigint", nullable: false),
                    RequiredInUnitOfMeasureId = table.Column<long>(type: "bigint", nullable: false),
                    NameOfUnitOfMeasureId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RequiredQuantity = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientDish", x => new { x.IngredientId, x.DishId });
                    table.ForeignKey(
                        name: "FK_IngredientDish_Dishes_DishId",
                        column: x => x.DishId,
                        principalSchema: "orders",
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientDish_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "orders",
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishMenu",
                schema: "orders",
                columns: table => new
                {
                    DishId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PromotionPriceOfferId = table.Column<int>(type: "int", nullable: true),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishMenu", x => new { x.MenuId, x.Version, x.DishId });
                    table.ForeignKey(
                        name: "FK_DishMenu_Dishes_DishId",
                        column: x => x.DishId,
                        principalSchema: "orders",
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishMenu_Menus_MenuId_Version",
                        columns: x => new { x.MenuId, x.Version },
                        principalSchema: "orders",
                        principalTable: "Menus",
                        principalColumns: new[] { "MenuId", "Version" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishMenu_PriceOffers_PromotionPriceOfferId",
                        column: x => x.PromotionPriceOfferId,
                        principalSchema: "orders",
                        principalTable: "PriceOffers",
                        principalColumn: "PriceOfferId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "orders",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    DishFromMenuMenuId = table.Column<long>(type: "bigint", nullable: true),
                    DishFromMenuVersion = table.Column<int>(type: "int", nullable: true),
                    DishFromMenuDishId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ItemTotalSum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    AppliedPriceOfferPriceOfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_DishMenu_DishFromMenuMenuId_DishFromMenuVersion_DishFromMenuDishId",
                        columns: x => new { x.DishFromMenuMenuId, x.DishFromMenuVersion, x.DishFromMenuDishId },
                        principalSchema: "orders",
                        principalTable: "DishMenu",
                        principalColumns: new[] { "MenuId", "Version", "DishId" });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "orders",
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderItems_PriceOffers_AppliedPriceOfferPriceOfferId",
                        column: x => x.AppliedPriceOfferPriceOfferId,
                        principalSchema: "orders",
                        principalTable: "PriceOffers",
                        principalColumn: "PriceOfferId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishMenu_DishId",
                schema: "orders",
                table: "DishMenu",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_DishMenu_PromotionPriceOfferId",
                schema: "orders",
                table: "DishMenu",
                column: "PromotionPriceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDish_DishId",
                schema: "orders",
                table: "IngredientDish",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AppliedPriceOfferPriceOfferId",
                schema: "orders",
                table: "OrderItems",
                column: "AppliedPriceOfferPriceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DishFromMenuMenuId_DishFromMenuVersion_DishFromMenuDishId",
                schema: "orders",
                table: "OrderItems",
                columns: new[] { "DishFromMenuMenuId", "DishFromMenuVersion", "DishFromMenuDishId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "orders",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientDish",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Ingredients",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "DishMenu",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Dishes",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "PriceOffers",
                schema: "orders");
        }
    }
}
