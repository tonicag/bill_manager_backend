using BillManager.Model;
using BillManager.Model.DTO;

namespace BillManager.Service.IService
{
    public interface IProductService
    {
        public Task<ProductDTO> CreateProduct(ProductDTO productRequest);
        public Task<ProductDTO> UpdateProduct(ProductDTO productRequest);
        public Task<string> DeleteProduct(string productId);

        public Task<ProductDTO> GetProductById(string productId);
        public Task<PaginationDTO<ProductDTO>> GetAllProducts(int page, int itemsPerPage, string searchKey, string userId);
    }
}
