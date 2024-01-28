using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityManager_Fun.Migrations
{
    public partial class PaymentDateImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "DateOfCreation");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEnd",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLatestModification",
                table: "Payments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfEnd",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DateOfLatestModification",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "DateOfCreation",
                table: "Payments",
                newName: "Date");
        }
    }
}
