namespace BillManager.Model.DTO
{
    public class BillUpdateRequestDTO
    {
        public string BillId { get; set; }
        public string SellerCompanyId { get; set; }
        public string BuyerCompanyId { get; set; }
    }
}
