using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class C1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountPin",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountPin",
                table: "Customers");
        }
    }
}
