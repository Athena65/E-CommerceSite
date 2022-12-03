using ClientSide.Services;
using ClientSide.Shared;
using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Pages
{
    public partial class CreateProduct
    {
        private Product _product =new Product();
        private SuccessNotification _notification;
        [Inject]
        public IProductHttpService? ProductRepo { get; set; }
        private async void Create()
        {
            await ProductRepo.CreateProduct(_product);
            _notification.Show();

        }
        private void AssignImageUrl(string imageUrl)=> _product.ImageUrl = imageUrl;
        
           
        
    }
}
