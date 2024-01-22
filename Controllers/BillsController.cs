using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillManager.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillsService _billsService;
        protected ResponseDTO _responseDTO;
        public BillsController(IBillsService billsService)
        {
            _billsService = billsService;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost("create")]
        public async Task<ResponseDTO> CreateBill([FromBody] BillCreateRequestDTO requestDTO)
        {
            var result = await _billsService.CreateBill(requestDTO);
            if (result == null)
            {
                _responseDTO.Message = "There was an error!";
                _responseDTO.IsSuccess = false;
                return _responseDTO;
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Result = result;
            return _responseDTO;
        }
        [HttpDelete("{billId}")]
        public async Task<ResponseDTO> DeleteBill(string billId)
        {
            var result = await _billsService.DeleteBill(billId);
            if (result == false)
            {
                _responseDTO.Message = "There was an error!";
                _responseDTO.IsSuccess = false;
                return _responseDTO;
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Result = result;
            return _responseDTO;
        }
    }
}
