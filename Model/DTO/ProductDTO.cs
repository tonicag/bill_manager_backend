namespace BillManager.Model.DTO
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string CompanyId { get; set; }
        public double Price { get; set; }
    }
}
