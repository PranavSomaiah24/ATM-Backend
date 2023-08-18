using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class T2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Balances_BalanceAccountNumber",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BalanceAccountNumber",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TransactionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BalanceAccountNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountNumber",
                table: "Customers",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Balances_AccountNumber",
                table: "Customers",
                column: "AccountNumber",
                principalTable: "Balances",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Balances_AccountNumber",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AccountNumber",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "BalanceAccountNumber",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BalanceAccountNumber",
                table: "Customers",
                column: "BalanceAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TransactionId",
                table: "Customers",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Balances_BalanceAccountNumber",
                table: "Customers",
                column: "BalanceAccountNumber",
                principalTable: "Balances",
                principalColumn: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
