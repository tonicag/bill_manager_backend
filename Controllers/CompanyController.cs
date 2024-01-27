using BillManager.Model.DTO;
using BillManager.Service;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillManager.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        protected ResponseDTO _responseDTO;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ResponseDTO> GetAll([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string searchKey = "")
        {
            var result = await _companyService.GetAll(page, itemsPerPage, searchKey);
            if (result != null)
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.Result = result;
                return _responseDTO;
            }
            _responseDTO.IsSuccess = false;
            return _responseDTO;
        }
    }
}
