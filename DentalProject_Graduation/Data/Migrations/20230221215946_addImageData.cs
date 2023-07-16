using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class addImageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                schema: "Security",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageUser",
                schema: "Security",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUser",
                schema: "Security",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
