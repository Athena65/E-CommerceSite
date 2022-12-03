using ClientSide.Services;
using ClientSide.Shared;
using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Pages
{
    public partial class UpdateProduct
    {
        private Product _product;
        private SuccessNotification _notification;
        [Inject]
        IProductHttpService Repo { get; set; }
        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _product = await Repo.GetProductById(Id);
        }
        private async Task Update()
        {
            await Repo.Update(_product);
            _notification.Show();
        }
        private void AssignImageUrl(string imageUrl) => _product.ImageUrl = imageUrl;   
    }
}
