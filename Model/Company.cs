using Microsoft.AspNetCore.Identity;

namespace BillManager.Model
{
    public class Company : IdentityUser
    {
        public string Name { get; set; }
        public string Cif { get; set; }
        public string RegistrationNumber { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }


        public ICollection<Product> Products { get; set; }
        public ICollection<Bill> SellerBills { get; set; }
        public ICollection<Bill> BuyerBills { get; set; }


    }
}
