using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowMeterTeamProject.Migrations
{
    /// <inheritdoc />
    public partial class AddGasField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gas",
                table: "accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gas",
                table: "accounts");
        }
    }
}
