using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowMeterTeamProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    PersonalAccount = table.Column<string>(type: "text", nullable: false),
                    HotWater = table.Column<decimal>(type: "numeric", nullable: true),
                    ColdWater = table.Column<decimal>(type: "numeric", nullable: true),
                    Heating = table.Column<decimal>(type: "numeric", nullable: true),
                    Electricity = table.Column<decimal>(type: "numeric", nullable: true),
                    PublicService = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.PersonalAccount);
                });

            migrationBuilder.CreateTable(
                name: "consumers",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "integer", nullable: false),
                    ConsumersId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonalAccount = table.Column<string>(type: "text", nullable: false),
                    Flat = table.Column<int>(type: "integer", nullable: true),
                    ConsumerOwner = table.Column<string>(type: "text", nullable: false),
                    HeatingArea = table.Column<int>(type: "integer", nullable: true),
                    NumberOfPersons = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumers", x => x.ConsumersId);
                });

            migrationBuilder.CreateTable(
                name: "counters",
                columns: table => new
                {
                    CountersId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreviousIndicator = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentIndicator = table.Column<decimal>(type: "numeric", nullable: true),
                    Account = table.Column<string>(type: "text", nullable: false),
                    TypeOfAccount = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_counters", x => x.CountersId);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeLogin = table.Column<string>(type: "text", nullable: false),
                    EmployeePassword = table.Column<string>(type: "text", nullable: false),
                    TypeOfUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeLogin);
                });

            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HouseAddress = table.Column<string>(type: "text", nullable: false),
                    HeatingAreaOfHouse = table.Column<int>(type: "integer", nullable: true),
                    NumberOfFlat = table.Column<int>(type: "integer", nullable: true),
                    NumberOfResidents = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.HouseId);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HouseId = table.Column<int>(type: "integer", nullable: true),
                    TypeOfAccount = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.ServiceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "consumers");

            migrationBuilder.DropTable(
                name: "counters");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "houses");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
