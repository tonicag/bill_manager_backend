using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BillManager.Controllers
{
    [Route("api/billEntry")]
    [ApiController]
    public class BillEntryController : ControllerBase
    {
        private readonly IBillEntryService _billEntryService;
        protected ResponseDTO _responseDTO;
        public BillEntryController(IBillEntryService billEntryService)
        {
            _billEntryService = billEntryService;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost("create")]
        public async Task<ResponseDTO> Create([FromBody] BillEntryDTO billEntry)
        {
            var result = await _billEntryService.CreateBillEntry(billEntry);
            if (result != null)
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.Result = result;
                return _responseDTO;
            }
            _responseDTO.IsSuccess = false;
            _responseDTO.Message = "Error";
            return _responseDTO;
        }
        [HttpDelete("{billEntryId}")]
        public async Task<ResponseDTO> Delete(string billEntryId)
        {
            var result = await _billEntryService.DeleteBillEntry(billEntryId);
            if (result)
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.Result = result;
                return _responseDTO;
            }
            _responseDTO.IsSuccess = false;
            _responseDTO.Message = "Error";
            return _responseDTO;
        }
    }
}
