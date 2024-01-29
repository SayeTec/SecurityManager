using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityManager_Fun.Migrations
{
    public partial class PaymentsImprovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentDeductions_Deductions_DeductionID",
                table: "paymentDeductions");

            migrationBuilder.DropForeignKey(
                name: "FK_paymentDeductions_Payments_PaymentID",
                table: "paymentDeductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymentDeductions",
                table: "paymentDeductions");

            migrationBuilder.RenameTable(
                name: "paymentDeductions",
                newName: "PaymentDeductions");

            migrationBuilder.RenameIndex(
                name: "IX_paymentDeductions_PaymentID",
                table: "PaymentDeductions",
                newName: "IX_PaymentDeductions_PaymentID");

            migrationBuilder.RenameIndex(
                name: "IX_paymentDeductions_DeductionID",
                table: "PaymentDeductions",
                newName: "IX_PaymentDeductions_DeductionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDeductions",
                table: "PaymentDeductions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDeductions_Deductions_DeductionID",
                table: "PaymentDeductions",
                column: "DeductionID",
                principalTable: "Deductions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDeductions_Payments_PaymentID",
                table: "PaymentDeductions",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDeductions_Deductions_DeductionID",
                table: "PaymentDeductions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDeductions_Payments_PaymentID",
                table: "PaymentDeductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDeductions",
                table: "PaymentDeductions");

            migrationBuilder.RenameTable(
                name: "PaymentDeductions",
                newName: "paymentDeductions");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDeductions_PaymentID",
                table: "paymentDeductions",
                newName: "IX_paymentDeductions_PaymentID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDeductions_DeductionID",
                table: "paymentDeductions",
                newName: "IX_paymentDeductions_DeductionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymentDeductions",
                table: "paymentDeductions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentDeductions_Deductions_DeductionID",
                table: "paymentDeductions",
                column: "DeductionID",
                principalTable: "Deductions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_paymentDeductions_Payments_PaymentID",
                table: "paymentDeductions",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
