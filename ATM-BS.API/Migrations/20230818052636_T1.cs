using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class T1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Transactions_TransactionAccountNumber",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TransactionAccountNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TransactionAccountNumber",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "IX_Customers_TransactionId",
                table: "Customers",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Transactions_TransactionId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_TransactionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "TransactionAccountNumber",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TransactionAccountNumber",
                table: "Customers",
                column: "TransactionAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Transactions_TransactionAccountNumber",
                table: "Customers",
                column: "TransactionAccountNumber",
                principalTable: "Transactions",
                principalColumn: "AccountNumber");
        }
    }
}
