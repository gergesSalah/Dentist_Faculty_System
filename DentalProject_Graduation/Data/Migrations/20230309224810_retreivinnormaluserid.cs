﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class retreivinnormaluserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Patients",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Patients_UserId",
            //    table: "Patients",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Patients_Users_UserId",
            //    table: "Patients",
            //    column: "UserId",
            //    principalSchema: "Security",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Patients_Users_UserId",
            //    table: "Patients");

            //migrationBuilder.DropIndex(
            //    name: "IX_Patients_UserId",
            //    table: "Patients");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Patients");
        }
    }
}