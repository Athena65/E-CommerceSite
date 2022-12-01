using BlazorServerSide.Data;
using BlazorServerSide.Paging;
using BlazorServerSide.Services.Extensions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerSide.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product newProduct)
        {
            newProduct.Id = Guid.NewGuid(); 
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;  
        }

        public async Task<PagedList<Product>> GetAllProducts(ProductParameters productParameters)
        {
            var products = await _context.Products
                .Search(productParameters.SearchTerm)
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            return PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.Where(x=>x.Id==id).FirstOrDefaultAsync(); 
        }

        public async Task<Product> RemoveProduct(Guid id)
        {
            var removed = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            _context.Products.Remove(removed);
            await _context.SaveChangesAsync();

            return removed;
        }

        public async Task<Product> Update(Guid id, Product product)
        {
            var newProduct= await _context.Products.Where(x=>x.Id==id).FirstOrDefaultAsync();

            newProduct.Name = product.Name;
            newProduct.Supplier= product.Supplier;  
            newProduct.Price = product.Price;   
            newProduct.ImageUrl = product.ImageUrl; 

            await _context.SaveChangesAsync();
            return newProduct;
        }
    }
}
