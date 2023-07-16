using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class alarmTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cases_PatientId",
                table: "Cases");

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    IdDentail = table.Column<int>(type: "int", nullable: false),
                    IdDiseaase = table.Column<int>(type: "int", nullable: false),
                    ApplyOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => new { x.IdDentail, x.IdDiseaase });
                    table.ForeignKey(
                        name: "FK_Alarms_DentalStudents_IdDentail",
                        column: x => x.IdDentail,
                        principalTable: "DentalStudents",
                        principalColumn: "DentalStudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alarms_Diseases_IdDiseaase",
                        column: x => x.IdDiseaase,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PatientId",
                table: "Cases",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_IdDiseaase",
                table: "Alarms",
                column: "IdDiseaase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Cases_PatientId",
                table: "Cases");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PatientId",
                table: "Cases",
                column: "PatientId",
                unique: true);
        }
    }
}
