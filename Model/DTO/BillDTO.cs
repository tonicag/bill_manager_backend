namespace BillManager.Model.DTO
{
    public class BillDTO
    {
        public string BillId { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice_NoVAT { get; set; }
        public double? TotalPrice_VAT { get; set; }

        public CompanyDTO SellerCompany { get; set; }
        public CompanyDTO BuyerCompany { get; set; }

        public List<BillEntryDTO> BillEntries { get; set; }
    }
}
