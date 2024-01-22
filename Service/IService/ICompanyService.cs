using BillManager.Model.DTO;

namespace BillManager.Service.IService
{
    public interface ICompanyService
    {
        public Task<List<CompanyDTO>> GetAll();
    }
}
