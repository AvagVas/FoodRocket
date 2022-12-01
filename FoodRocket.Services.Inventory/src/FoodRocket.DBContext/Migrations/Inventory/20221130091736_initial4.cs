using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Inventory
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnitOfMeasure_Products_ProductId",
                schema: "inventory",
                table: "ProductUnitOfMeasure");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnitOfMeasure_UnitOfMeasures_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUnitOfMeasure",
                schema: "inventory",
                table: "ProductUnitOfMeasure");

            migrationBuilder.RenameTable(
                name: "ProductUnitOfMeasure",
                schema: "inventory",
                newName: "ProductUnitOfMeasures",
                newSchema: "inventory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUnitOfMeasure_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasures",
                newName: "IX_ProductUnitOfMeasures_UnitOfMeasureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUnitOfMeasures",
                schema: "inventory",
                table: "ProductUnitOfMeasures",
                columns: new[] { "ProductId", "UnitOfMeasureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnitOfMeasures_Products_ProductId",
                schema: "inventory",
                table: "ProductUnitOfMeasures",
                column: "ProductId",
                principalSchema: "inventory",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnitOfMeasures_UnitOfMeasures_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasures",
                column: "UnitOfMeasureId",
                principalSchema: "inventory",
                principalTable: "UnitOfMeasures",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnitOfMeasures_Products_ProductId",
                schema: "inventory",
                table: "ProductUnitOfMeasures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnitOfMeasures_UnitOfMeasures_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUnitOfMeasures",
                schema: "inventory",
                table: "ProductUnitOfMeasures");

            migrationBuilder.RenameTable(
                name: "ProductUnitOfMeasures",
                schema: "inventory",
                newName: "ProductUnitOfMeasure",
                newSchema: "inventory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUnitOfMeasures_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasure",
                newName: "IX_ProductUnitOfMeasure_UnitOfMeasureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUnitOfMeasure",
                schema: "inventory",
                table: "ProductUnitOfMeasure",
                columns: new[] { "ProductId", "UnitOfMeasureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnitOfMeasure_Products_ProductId",
                schema: "inventory",
                table: "ProductUnitOfMeasure",
                column: "ProductId",
                principalSchema: "inventory",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnitOfMeasure_UnitOfMeasures_UnitOfMeasureId",
                schema: "inventory",
                table: "ProductUnitOfMeasure",
                column: "UnitOfMeasureId",
                principalSchema: "inventory",
                principalTable: "UnitOfMeasures",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
