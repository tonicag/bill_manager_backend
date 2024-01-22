using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillManager.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICompanyAuthService _authService;
        protected ResponseDTO _responseDTO;
        public AuthenticationController(ICompanyAuthService authService)
        {
            _authService = authService;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost("register")]
        public async Task<ResponseDTO> Register([FromBody] RegistrationRequestDTO requestDTO)
        {
            var result = await _authService.Register(requestDTO);

            if (result == "")
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.Result = "Successfully registered!";
                return _responseDTO;
            }

            _responseDTO.IsSuccess = false;
            _responseDTO.Message = result;
            return _responseDTO;
        }
        [HttpPost("login")]
        public async Task<ResponseDTO> Login([FromBody] LoginRequestDTO requestDTO)
        {
            var result = await _authService.Login(requestDTO);

            _responseDTO.IsSuccess = true;
            if (result.Company == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "Invalid credentials!";
            }

            _responseDTO.Result = result;
            return _responseDTO;

        }
    }
}
