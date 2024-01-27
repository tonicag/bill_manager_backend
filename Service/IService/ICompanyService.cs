using BillManager.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BillManager.Service.IService
{
    public interface ICompanyService
    {
        public Task<PaginationDTO<CompanyDTO>> GetAll(int page, int itemsPerPage, string searchKey);
    }
}
