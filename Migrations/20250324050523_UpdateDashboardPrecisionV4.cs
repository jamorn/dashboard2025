using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDashboardPrecisionV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Giveaway",
                table: "Dashboards",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Dashboard_Availability",
                table: "Dashboards",
                sql: "[Availability] >= 0 AND [Availability] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Dashboard_Giveaway",
                table: "Dashboards",
                sql: "[Giveaway] >= 0 AND [Giveaway] <= 25.30");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Dashboard_OEE",
                table: "Dashboards",
                sql: "[OEE] >= 0 AND [OEE] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Dashboard_Performance",
                table: "Dashboards",
                sql: "[Performance] >= 0 AND [Performance] <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Dashboard_Quality",
                table: "Dashboards",
                sql: "[Quality] >= 0 AND [Quality] <= 100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Dashboard_Availability",
                table: "Dashboards");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Dashboard_Giveaway",
                table: "Dashboards");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Dashboard_OEE",
                table: "Dashboards");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Dashboard_Performance",
                table: "Dashboards");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Dashboard_Quality",
                table: "Dashboards");

            migrationBuilder.AlterColumn<decimal>(
                name: "Giveaway",
                table: "Dashboards",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
