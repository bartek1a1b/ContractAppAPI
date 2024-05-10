namespace ContractAppAPI.Dto
{
    public class PdfDto
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public int AnnexToTheContractId { get; set; }
    }
}