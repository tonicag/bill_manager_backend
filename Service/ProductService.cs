using AutoMapper;
using BillManager.Data;
using BillManager.Model;
using BillManager.Model.DTO;
using BillManager.Service.IService;

namespace BillManager.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProductService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productRequest)
        {
            try
            {
                var product = await _db.Products.AddAsync(new Product
                {
                    CompanyId = productRequest.CompanyId,
                    Description = productRequest.Description,
                    Name = productRequest.Name,
                    Price = productRequest.Price
                });
                await _db.SaveChangesAsync();
                productRequest.Id = product.Entity.Id;
                return productRequest;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<string> DeleteProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationDTO<ProductDTO>> GetAllProducts(int page, int itemsPerPage, string searchKey, string userId)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(p => p.Name.Contains(searchKey));
            }
            query = query.Where(p => p.CompanyId == userId);
            var totalItems = query.Count();
            var items = query.Skip((page - 1) * itemsPerPage)
                             .Take(itemsPerPage).Select(item => _mapper.Map<ProductDTO>(item))
                             .ToList();
            return new PaginationDTO<ProductDTO> { Items = items, ItemsPerPage = itemsPerPage, Page = page, TotalPages = totalItems };
        }


        public Task<ProductDTO> GetProductById(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> UpdateProduct(ProductDTO productRequest)
        {
            throw new NotImplementedException();
        }
    }
}
