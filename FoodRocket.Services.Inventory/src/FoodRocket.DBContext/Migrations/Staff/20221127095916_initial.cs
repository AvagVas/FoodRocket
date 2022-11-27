using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Staff
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "staff");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "staff",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "staff",
                columns: table => new
                {
                    ManagerId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                    table.ForeignKey(
                        name: "FK_Managers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "staff",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                schema: "staff",
                columns: table => new
                {
                    WaiterId = table.Column<long>(type: "bigint", nullable: false),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerId = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waiters", x => x.WaiterId);
                    table.ForeignKey(
                        name: "FK_Waiters_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "staff",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Waiters_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "staff",
                        principalTable: "Managers",
                        principalColumn: "ManagerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_EmployeeId",
                schema: "staff",
                table: "Managers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Waiters_EmployeeId",
                schema: "staff",
                table: "Waiters",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Waiters_ManagerId",
                schema: "staff",
                table: "Waiters",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Waiters",
                schema: "staff");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "staff");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "staff");
        }
    }
}
