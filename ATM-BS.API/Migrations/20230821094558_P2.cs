using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class P2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "FromAccountNumber",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToAccountNumber",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAccountNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAccountNumber",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "TId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardNumber",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Transactions",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Transactions",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TId");
        }
    }
}
