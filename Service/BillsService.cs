using AutoMapper;
using BillManager.Data;
using BillManager.Model;
using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace BillManager.Service
{
    public class BillsService : IBillsService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BillsService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BillDTO> CreateBill(BillCreateRequestDTO requestDTO)
        {
            try
            {

                var bill = await _db.Bills.AddAsync(
                    new Bill
                    {
                        BuyerCompanyId = requestDTO.BuyerCompanyId,
                        SellerCompanyId = requestDTO.SellerCompanyId,
                        Date = DateTime.UtcNow,
                    }
                );

                await _db.SaveChangesAsync();

                var entriesToBeAdded = requestDTO.BillEntries.Select(
                        b => new BillEntry
                        {
                            BillId = bill.Entity.BillId,
                            ProductId = b.ProductId,
                            Quantity = b.Quantity,
                        });

                await _db.BillEntries.AddRangeAsync(entriesToBeAdded);
                await _db.SaveChangesAsync();

                var billEntries = await _db.BillEntries
                            .Include(be => be.Product)
                            .Where(be => be.BillId == bill.Entity.BillId)
                            .ToListAsync();
                double total_cost = 0;

                foreach (BillEntry b in billEntries)
                {
                    total_cost = b.Quantity * b.Product.Price;
                }

                bill.Entity.TotalPrice_NoVAT = total_cost;

                _db.Update(bill.Entity);
                await _db.SaveChangesAsync();

                var buyerCompany = await _db.Companies.FirstOrDefaultAsync(c => c.Id == bill.Entity.BuyerCompanyId);
                var sellerCompany = await _db.Companies.FirstOrDefaultAsync(c => c.Id == bill.Entity.SellerCompanyId);

                return new BillDTO
                {
                    BillEntries = billEntries.Select(be => _mapper.Map<BillEntryDTO>(be)).ToList(),
                    BillId = bill.Entity.BillId,
                    Date = bill.Entity.Date,
                    BuyerCompany = _mapper.Map<CompanyDTO>(buyerCompany),
                    SellerCompany = _mapper.Map<CompanyDTO>(sellerCompany),
                    TotalPrice_NoVAT = bill.Entity.TotalPrice_NoVAT,
                    TotalPrice_VAT = bill.Entity.TotalPrice_VAT
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteBill(string billId)
        {
            var bill = _db.Bills.FirstOrDefault(b => b.BillId == billId);
            if (bill == null)
            {
                return false;
            }
            try
            {
                _db.Bills.Remove(bill);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Task<List<BillDTO>> GetAllBills(string billId)
        {

            throw new NotImplementedException();
        }

        public Task<Bill> GetBill(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetBillById(string billId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBill(BillUpdateRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

    }
}
