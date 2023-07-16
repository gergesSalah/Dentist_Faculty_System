using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalProject_Graduation.Data.Migrations
{
    public partial class PK_CaseInform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cases_PatientId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseInformation",
                table: "CaseInformation");

            migrationBuilder.AddColumn<int>(
                name: "CaseInformationId",
                table: "CaseInformation",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseInformation",
                table: "CaseInformation",
                column: "CaseInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PatientId",
                table: "Cases",
                column: "PatientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cases_PatientId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseInformation",
                table: "CaseInformation");

            migrationBuilder.DropColumn(
                name: "CaseInformationId",
                table: "CaseInformation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseInformation",
                table: "CaseInformation",
                columns: new[] { "CaseId", "DentalStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PatientId",
                table: "Cases",
                column: "PatientId");
        }
    }
}
