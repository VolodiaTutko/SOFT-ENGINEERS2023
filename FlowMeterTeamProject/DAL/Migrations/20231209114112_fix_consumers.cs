using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowMeterTeamProject.Migrations
{
    /// <inheritdoc />
    public partial class fixconsumers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "consumers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPersons",
                table: "consumers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "consumers");

            migrationBuilder.DropColumn(
                name: "NumberOfPersons",
                table: "consumers");
        }
    }
}
