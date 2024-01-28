using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityManager_Fun.Migrations
{
    public partial class PaymentValueImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Payments",
                newName: "FinalValue");

            migrationBuilder.AddColumn<decimal>(
                name: "DeductionsValue",
                table: "Payments",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultValue",
                table: "Payments",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeductionsValue",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "FinalValue",
                table: "Payments",
                newName: "Value");
        }
    }
}
