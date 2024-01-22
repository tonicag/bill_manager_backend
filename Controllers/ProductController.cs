using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace BillManager.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;
        protected ResponseDTO _responseDTO;
        public ProductController(IProductService productsService)
        {
            _productsService = productsService;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost("create")]
        [Authorize]
        public async Task<ResponseDTO> CreateProduct([FromBody] ProductDTO productRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            productRequest.CompanyId = userId;
            var result = await _productsService.CreateProduct(productRequest);

            if (result == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "There has been an error!";
                return _responseDTO;
            }

            _responseDTO.Result = result;
            _responseDTO.IsSuccess = true;
            return _responseDTO;
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<ResponseDTO> GetAllProducts([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string searchKey = "")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productsService.GetAllProducts(page, itemsPerPage, searchKey, userId);

            if (result == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "There has been an error!";
                return _responseDTO;
            }

            _responseDTO.Result = result;
            _responseDTO.IsSuccess = true;
            return _responseDTO;
        }
    }
}
