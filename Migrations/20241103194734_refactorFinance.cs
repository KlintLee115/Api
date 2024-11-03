using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class refactorFinance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_FinancialInfo_FinancialInfo",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_FinancialInfo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FinancialInfo",
                table: "Customers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialInfo_Customers_CustomerId",
                table: "FinancialInfo");

            migrationBuilder.DropIndex(
                name: "IX_FinancialInfo_CustomerId",
                table: "FinancialInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "FinancialInfo",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FinancialInfo",
                table: "Customers",
                column: "FinancialInfo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_FinancialInfo_FinancialInfo",
                table: "Customers",
                column: "FinancialInfo",
                principalTable: "FinancialInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
