using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractAppAPI.Migrations
{
    public partial class HasPdfAnnex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPdf",
                table: "AnnexToTheContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPdf",
                table: "AnnexToTheContracts");
        }
    }
}
