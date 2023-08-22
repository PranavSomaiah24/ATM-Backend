using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class P4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FromAccountBalance",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ToAccountBalance",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAccountBalance",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAccountBalance",
                table: "Transactions");
        }
    }
}
