using AutoMapper;
using BillManager.Data;
using BillManager.Model;
using BillManager.Model.DTO;
using BillManager.Service.IService;

namespace BillManager.Service
{
    public class BillEntryService : IBillEntryService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BillEntryService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BillEntryDTO> CreateBillEntry(BillEntryDTO billEntry)
        {
            try
            {
                var be = await _db.BillEntries.AddAsync(_mapper.Map<BillEntry>(billEntry));
                return _mapper.Map<BillEntryDTO>(be);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteBillEntry(string billEntryId)
        {
            try
            {
                var be = _db.BillEntries.FirstOrDefault((be) => be.Id == billEntryId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<BillEntryDTO> UpdateBillEntry(BillEntryDTO billEntry)
        {
            throw new NotImplementedException();
        }
    }
}
