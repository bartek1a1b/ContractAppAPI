namespace ContractAppAPI.Models
{
    public class AnnexToTheContract
    {
        public int Id { get; set; }
        public int AnnexNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfConclusion { get; set; }
        public string Description { get; set; }
        public string Contractor { get; set; }
        public string Signatory { get; set; }
        public Contract Contract { get; set; }
        public ICollection<Pdf> Pdfs { get; set; }
    }
}