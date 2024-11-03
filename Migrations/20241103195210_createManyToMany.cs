using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class createManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialInfo_Customers_CustomerId",
                table: "FinancialInfo");

            migrationBuilder.DropIndex(
                name: "IX_FinancialInfo_CustomerId",
                table: "FinancialInfo");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "FinancialInfo");

            migrationBuilder.DropColumn(
                name: "FinancialInfoId",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "CustomerFinancialInfo",
                columns: table => new
                {
                    CustomersId = table.Column<Guid>(type: "uuid", nullable: false),
                    FinancialInfosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFinancialInfo", x => new { x.CustomersId, x.FinancialInfosId });
                    table.ForeignKey(
                        name: "FK_CustomerFinancialInfo_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFinancialInfo_FinancialInfo_FinancialInfosId",
                        column: x => x.FinancialInfosId,
                        principalTable: "FinancialInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFinancialInfo_FinancialInfosId",
                table: "CustomerFinancialInfo",
                column: "FinancialInfosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFinancialInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "FinancialInfo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FinancialInfoId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInfo_CustomerId",
                table: "FinancialInfo",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialInfo_Customers_CustomerId",
                table: "FinancialInfo",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
