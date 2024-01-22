using BillManager.Data;
using BillManager.Model;
using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BillManager.Service
{
    public class CompanyAuthService : ICompanyAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Company> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public CompanyAuthService(AppDbContext appDbContext,
            UserManager<Company> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            this._db = appDbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<bool> AssignRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
        {
            var company = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(company, loginRequestDto.Password);

            if (company == null || isValid == false)
            {
                return new LoginResponseDTO() { Company = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(company);
            var token = _jwtTokenGenerator.GenerateToken(company, roles);

            CompanyDTO companyDTO = new CompanyDTO
            {
                Id = company.Id,
                Email = company.Email,
                Name = company.Name,
                Address = company.Address,
                BankAccount = company.BankAccount,
                Cif = company.Cif,
                PhoneNumber = company.PhoneNumber,
                RegistrationNumber = company.RegistrationNumber,
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Company = companyDTO,
                Token = token
            };
            return loginResponseDTO;
        }

        public async Task<string> Register(RegistrationRequestDTO request)
        {
            Company user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                NormalizedEmail = request.Email.ToLower(),
                Name = request.Name,
                RegistrationNumber = request.RegistrationNumber,
                Address = request.Address,
                BankAccount = request.BankAccount,
                Cif = request.Cif,
                PhoneNumber = request.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
