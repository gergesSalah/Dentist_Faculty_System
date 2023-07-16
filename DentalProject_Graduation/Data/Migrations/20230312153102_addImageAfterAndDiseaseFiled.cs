using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class addImageAfterAndDiseaseFiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DiseasePicture",
                table: "Cases",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoAfter",
                table: "CaseInformation",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiseasePicture",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "PhotoAfter",
                table: "CaseInformation");
        }
    }
}
