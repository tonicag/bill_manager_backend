using AutoMapper;
using BillManager.Data;
using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace BillManager.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CompanyService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CompanyDTO>> GetAll()
        {
            var companies = await _db.Companies.Take(50).Select(c => _mapper.Map<CompanyDTO>(c)).ToListAsync();

            return companies;
        }
    }
}
