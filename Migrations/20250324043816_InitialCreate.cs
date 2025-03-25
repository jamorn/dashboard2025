using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Availability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Performance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quality = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OEE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giveaway = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "RemarkItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemarkItems", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "RemarkItems");
        }
    }
}
