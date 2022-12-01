using BlazorServerSide.Paging;
using Entities.Models;
using Entities.RequestFeatures;

namespace BlazorServerSide.Services
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product newProduct);
        public Task<PagedList<Product>> GetAllProducts(ProductParameters productParameters);
        Task<Product> GetProductById(Guid id);

        Task<Product> RemoveProduct(Guid id);
        Task<Product> Update(Guid id, Product product);
        
    }
}
