using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTypeOnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeOnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypeTwos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeTwos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfConclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Contractor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signatory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pdf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractTypeOneId = table.Column<int>(type: "int", nullable: false),
                    ContractTypeTwoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTypeOnes_ContractTypeOneId",
                        column: x => x.ContractTypeOneId,
                        principalTable: "ContractTypeOnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTypeTwos_ContractTypeTwoId",
                        column: x => x.ContractTypeTwoId,
                        principalTable: "ContractTypeTwos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeOneId",
                table: "Contracts",
                column: "ContractTypeOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeTwoId",
                table: "Contracts",
                column: "ContractTypeTwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractTypeOnes");

            migrationBuilder.DropTable(
                name: "ContractTypeTwos");
        }
    }
}
