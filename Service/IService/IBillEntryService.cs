using BillManager.Model.DTO;

namespace BillManager.Service.IService
{
    public interface IBillEntryService
    {
        public Task<bool> DeleteBillEntry(string billEntryId);
        public Task<BillEntryDTO> CreateBillEntry(BillEntryDTO billEntry);
        public Task<BillEntryDTO> UpdateBillEntry(BillEntryDTO billEntry);
    }
}
