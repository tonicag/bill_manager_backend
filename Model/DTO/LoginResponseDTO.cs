namespace BillManager.Model.DTO
{
    public class LoginResponseDTO
    {
        public CompanyDTO? Company { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
