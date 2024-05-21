using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class EditTypeEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypeOnes_ContractTypeOneId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypeTwos_ContractTypeTwoId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "ContractTypeOneId",
                table: "ContractTypeTwos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPdf",
                table: "AnnexToTheContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeTwos_ContractTypeOneId",
                table: "ContractTypeTwos",
                column: "ContractTypeOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypeOnes_ContractTypeOneId",
                table: "Contracts",
                column: "ContractTypeOneId",
                principalTable: "ContractTypeOnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypeTwos_ContractTypeTwoId",
                table: "Contracts",
                column: "ContractTypeTwoId",
                principalTable: "ContractTypeTwos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTypeTwos_ContractTypeOnes_ContractTypeOneId",
                table: "ContractTypeTwos",
                column: "ContractTypeOneId",
                principalTable: "ContractTypeOnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypeOnes_ContractTypeOneId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractTypeTwos_ContractTypeTwoId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypeTwos_ContractTypeOnes_ContractTypeOneId",
                table: "ContractTypeTwos");

            migrationBuilder.DropIndex(
                name: "IX_ContractTypeTwos_ContractTypeOneId",
                table: "ContractTypeTwos");

            migrationBuilder.DropColumn(
                name: "ContractTypeOneId",
                table: "ContractTypeTwos");

            migrationBuilder.DropColumn(
                name: "HasPdf",
                table: "AnnexToTheContracts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypeOnes_ContractTypeOneId",
                table: "Contracts",
                column: "ContractTypeOneId",
                principalTable: "ContractTypeOnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractTypeTwos_ContractTypeTwoId",
                table: "Contracts",
                column: "ContractTypeTwoId",
                principalTable: "ContractTypeTwos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
