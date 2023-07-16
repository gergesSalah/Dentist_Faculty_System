using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class newvilad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Viald",
                table: "Cases",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Viald",
                table: "Cases");
        }
    }
}
