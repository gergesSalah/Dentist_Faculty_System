using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class removeImageFiledFormTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiseasePicture",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "PhotoAfter",
                table: "CaseInformation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiseasePicture",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoAfter",
                table: "CaseInformation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
