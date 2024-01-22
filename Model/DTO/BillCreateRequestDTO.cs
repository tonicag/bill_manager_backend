namespace BillManager.Model.DTO
{
    public class BillCreateRequestDTO
    {
        public string SellerCompanyId { get; set; }
        public string BuyerCompanyId { get; set; }
        public List<BillEntryDTO> BillEntries { get; set; }

    }
}
