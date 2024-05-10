using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class AddPdfv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Pdfs");

            migrationBuilder.DropColumn(
                name: "PdfId",
                table: "AnnexToTheContracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Pdfs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PdfId",
                table: "AnnexToTheContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
