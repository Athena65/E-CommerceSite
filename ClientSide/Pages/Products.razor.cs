using Entities.Models;
using ClientSide.Services;
using Microsoft.AspNetCore.Components;
using Entities.RequestFeatures;

namespace ClientSide.Pages
{
    public partial class Products:IDisposable
    {
        public List<Product> ProductList { get; set; }  = new List<Product>();
        public MetaData MetaData { get; set; }= new MetaData(); 
        private ProductParameters _productParameters = new ProductParameters();
        [Inject]
        public IProductHttpService ProductRepo { get; set; }
        [Inject]
        public InterceptorService Interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetProducts(); 

        }
        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProducts();
        }
        private async Task GetProducts()
        {
            var pagingResponse = await ProductRepo.GetAllProducts(_productParameters);
            ProductList = pagingResponse.Items.ToList();
            MetaData = pagingResponse.MetaData;
        }
        private async Task SearchChanged(string key)
        {
            Console.WriteLine(key);
            _productParameters.PageNumber = 1;
            _productParameters.SearchTerm = key;
            await GetProducts();
        }
        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _productParameters.OrderBy = orderBy;   
            await GetProducts();    
        }
        private async Task DeleteProduct(Guid id)
        {
            await ProductRepo.DeleteProduct(id);
            _productParameters.PageNumber = 1;
            await GetProducts();
        }

        public void Dispose() => Interceptor.DisposeEvent();

    }
}
