using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class AddPdfv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pdf",
                table: "AnnexToTheContracts");

            migrationBuilder.AddColumn<int>(
                name: "PdfId",
                table: "AnnexToTheContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfId",
                table: "AnnexToTheContracts");

            migrationBuilder.AddColumn<string>(
                name: "Pdf",
                table: "AnnexToTheContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
