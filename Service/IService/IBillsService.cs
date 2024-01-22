using BillManager.Model;
using BillManager.Model.DTO;

namespace BillManager.Service.IService
{
    public interface IBillsService
    {
        public Task<bool> GetBillById(string billId);
        public Task<List<BillDTO>> GetAllBills(string billId);

        public Task<BillDTO> CreateBill(BillCreateRequestDTO requestDTO);
        public Task<bool> UpdateBill(BillUpdateRequestDTO requestDTO);
        public Task<bool> DeleteBill(string billId);
        public Task<Bill> GetBill(string id);
    }
}
