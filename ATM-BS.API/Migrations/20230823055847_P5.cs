using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_BS.API.Migrations
{
    public partial class P5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "Admins");
        }
    }
}
