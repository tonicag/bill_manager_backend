namespace BillManager.Model.DTO
{
    public class RegistrationRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Cif { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BankAccount { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
