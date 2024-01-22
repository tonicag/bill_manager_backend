namespace BillManager.Model.DTO
{
    public class BillEntryDTO
    {
        public string Id { get; set; }
        public string BillId { get; set; }
        public string ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
