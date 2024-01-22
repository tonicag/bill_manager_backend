using System.ComponentModel.DataAnnotations;

namespace BillManager.Model
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string CompanyId { get; set; }
        public double Price { get; set; }
        public Company Company { get; set; }
        public ICollection<BillEntry> BillEntries { get; set; }

    }
}
