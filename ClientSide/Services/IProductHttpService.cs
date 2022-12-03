using ClientSide.Features;
using Entities.Models;
using Entities.RequestFeatures;

namespace ClientSide.Services
{
    public interface IProductHttpService
    {
        Task<PagingResponse<Product>> GetAllProducts(ProductParameters productParameters);
        Task<Product> CreateProduct(Product newProduct);
        Task<string> UploadProductImage(MultipartFormDataContent content);
        Task<Product> GetProductById(string id);
        Task<Product> Update(Product product);
        Task DeleteProduct(Guid id);
        
    }
}
