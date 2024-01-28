using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityManager_Fun.Migrations
{
    public partial class DeductionImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deductions_Countries_CountryID",
                table: "Deductions");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Deductions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Countries_CountryID",
                table: "Deductions",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deductions_Countries_CountryID",
                table: "Deductions");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Deductions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Countries_CountryID",
                table: "Deductions",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
