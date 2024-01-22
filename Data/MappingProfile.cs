using AutoMapper;
using BillManager.Model;
using BillManager.Model.DTO;
namespace BillManager.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>();
            CreateMap<BillEntry, BillEntryDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Bill, BillDTO>();

        }
    }
}
