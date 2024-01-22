using System.ComponentModel.DataAnnotations;

namespace BillManager.Model
{
    public class BillEntry
    {
        [Key]
        public string Id { get; set; }
        public string BillId { get; set; }
        public string ProductId { get; set; }
        public double Quantity { get; set; }
        public Bill Bill { get; set; }
        public Product Product { get; set; }
    }

}
