using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RemarkItems",
                table: "RemarkItems");

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 9);

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
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

            migrationBuilder.AddColumn<string>(
                name: "MachineLine",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "Machines",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Dashboards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePerson",
                table: "Dashboards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemarkItems",
                table: "RemarkItems",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Dashboards_MachineId_RecordDate",
                table: "Dashboards",
                columns: new[] { "MachineId", "RecordDate" });

            migrationBuilder.CreateTable(
                name: "UnitPLBG",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CostCenter = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPLBG", x => x.UnitId);
                    table.UniqueConstraint("AK_UnitPLBG_CostCenter", x => x.CostCenter);
                });

            migrationBuilder.CreateTable(
                name: "KPI",
                columns: table => new
                {
                    Item = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Waste_Pellet_Target = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Waste_Film_Target = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    GiveAway_Target = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    Oee_Target = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    GiveAwayMin = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    GiveAwayMax = table.Column<decimal>(type: "decimal(10,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPI", x => x.Item);
                    table.ForeignKey(
                        name: "FK_KPI_UnitPLBG_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitPLBG",
                        principalColumn: "CostCenter",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemarkItems_MachineId_RecordDate",
                table: "RemarkItems",
                columns: new[] { "MachineId", "RecordDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_UnitId",
                table: "Machines",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_MachineId_RecordDate",
                table: "Dashboards",
                columns: new[] { "MachineId", "RecordDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KPI_UnitId",
                table: "KPI",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Machines_MachineId",
                table: "Dashboards",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "MachineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_UnitPLBG_UnitId",
                table: "Machines",
                column: "UnitId",
                principalTable: "UnitPLBG",
                principalColumn: "CostCenter",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemarkItems_Dashboards_MachineId_RecordDate",
                table: "RemarkItems",
                columns: new[] { "MachineId", "RecordDate" },
                principalTable: "Dashboards",
                principalColumns: new[] { "MachineId", "RecordDate" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Machines_MachineId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_UnitPLBG_UnitId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_RemarkItems_Dashboards_MachineId_RecordDate",
                table: "RemarkItems");

            migrationBuilder.DropTable(
                name: "KPI");

            migrationBuilder.DropTable(
                name: "UnitPLBG");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RemarkItems",
                table: "RemarkItems");

            migrationBuilder.DropIndex(
                name: "IX_RemarkItems_MachineId_RecordDate",
                table: "RemarkItems");

            migrationBuilder.DropIndex(
                name: "IX_Machines_UnitId",
                table: "Machines");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Dashboards_MachineId_RecordDate",
                table: "Dashboards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_MachineId_RecordDate",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "MachineLine",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "ResponsiblePerson",
                table: "Dashboards");

            migrationBuilder.AlterColumn<string>(
                name: "MachineName",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_RemarkItems",
                table: "RemarkItems",
                columns: new[] { "MachineId", "Id" });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "MachineId", "MachineActive", "MachineClass", "MachineName" },
                values: new object[,]
                {
                    { 1, true, "g1", "PP12/A" },
                    { 2, true, "g1", "PP12/C" },
                    { 3, true, "g1", "PP3/A" },
                    { 4, true, "g1", "PP3/B" },
                    { 5, true, "g1", "PPE/C" },
                    { 6, true, "g1", "PPE/D" },
                    { 7, true, "g1", "PPC/A" },
                    { 8, true, "g1", "PPC/B" },
                    { 9, true, "g1", "HDPE/A" }
                });
        }
    }
}
