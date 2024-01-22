using BillManager.Model.DTO;

namespace BillManager.Service.IService
{
    public interface ICompanyAuthService
    {
        Task<string> Register(RegistrationRequestDTO request);
        //Task<string> Update(UpdateRequest request);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        //Task<UserDto> GetUserById(Guid id);
        //Task<bool> DeleteUser(Guid id);
        //Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
