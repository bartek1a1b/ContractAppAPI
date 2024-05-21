using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class EditContractTypeMappingFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractTypeTwos_ContractTypeOnes_ContractTypeOneId1",
                table: "ContractTypeTwos");

            migrationBuilder.DropIndex(
                name: "IX_ContractTypeTwos_ContractTypeOneId1",
                table: "ContractTypeTwos");

            migrationBuilder.DropColumn(
                name: "ContractTypeOneId1",
                table: "ContractTypeTwos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractTypeOneId1",
                table: "ContractTypeTwos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeTwos_ContractTypeOneId1",
                table: "ContractTypeTwos",
                column: "ContractTypeOneId1",
                unique: true,
                filter: "[ContractTypeOneId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTypeTwos_ContractTypeOnes_ContractTypeOneId1",
                table: "ContractTypeTwos",
                column: "ContractTypeOneId1",
                principalTable: "ContractTypeOnes",
                principalColumn: "Id");
        }
    }
}
