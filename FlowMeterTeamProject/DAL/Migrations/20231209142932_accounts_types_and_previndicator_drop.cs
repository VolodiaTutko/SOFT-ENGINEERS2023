using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowMeterTeamProject.Migrations
{
    /// <inheritdoc />
    public partial class accountstypesandprevindicatordrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousIndicator",
                table: "counters");

            migrationBuilder.AlterColumn<string>(
                name: "PublicService",
                table: "accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HotWater",
                table: "accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Heating",
                table: "accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Electricity",
                table: "accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColdWater",
                table: "accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PreviousIndicator",
                table: "counters",
                type: "numeric",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PublicService",
                table: "accounts",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HotWater",
                table: "accounts",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Heating",
                table: "accounts",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Electricity",
                table: "accounts",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ColdWater",
                table: "accounts",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
