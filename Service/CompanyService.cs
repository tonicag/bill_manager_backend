using AutoMapper;
using BillManager.Data;
using BillManager.Model.DTO;
using BillManager.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BillManager.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDTO _responseDTO;

        public CompanyService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        public async Task<PaginationDTO<CompanyDTO>> GetAll(int page, int itemsPerPage, string searchKey)
        {
            var query = _db.Companies.AsQueryable();

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey));
            }

            var totalItems = query.Count();
            var items = query.Skip((page - 1) * itemsPerPage)
                             .Take(itemsPerPage).Select(item => _mapper.Map<CompanyDTO>(item))
                             .ToList();
            return new PaginationDTO<CompanyDTO> { Items = items, ItemsPerPage = itemsPerPage, Page = page, TotalPages = totalItems != 0 ? (totalItems / itemsPerPage) + 1 : 0 };
        }
    }
}
