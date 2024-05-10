namespace ContractAppAPI.Dto
{
    public class ContractPdfDto
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public int ContractId { get; set; }
    }
}