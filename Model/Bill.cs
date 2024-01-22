namespace BillManager.Model
{
    public class Bill
    {
        public string BillId { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice_NoVAT { get; set; }
        public double? TotalPrice_VAT { get; set; }

        public string SellerCompanyId { get; set; }
        public Company SellerCompany { get; set; }
        public string BuyerCompanyId { get; set; }
        public Company BuyerCompany { get; set; }

        // Navigation property for BillEntry
        public ICollection<BillEntry> BillEntries { get; set; }
    }
}
