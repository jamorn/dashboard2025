using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMachineAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_UnitPLBG_UnitId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Machines",
                newName: "CostCenter");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_UnitId",
                table: "Machines",
                newName: "IX_Machines_CostCenter");

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "Machines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MachineLine",
                table: "Machines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MachineClass",
                table: "Machines",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_UnitPLBG_CostCenter",
                table: "Machines",
                column: "CostCenter",
                principalTable: "UnitPLBG",
                principalColumn: "CostCenter",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_UnitPLBG_CostCenter",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "CostCenter",
                table: "Machines",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_CostCenter",
                table: "Machines",
                newName: "IX_Machines_UnitId");

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MachineLine",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MachineClass",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_UnitPLBG_UnitId",
                table: "Machines",
                column: "UnitId",
                principalTable: "UnitPLBG",
                principalColumn: "CostCenter",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
