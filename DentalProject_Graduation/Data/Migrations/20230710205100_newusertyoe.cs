using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class newusertyoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "Security",
                table: "Users");
        }
    }
}
