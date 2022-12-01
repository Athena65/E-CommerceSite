using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Components.ProductTable
{
    public partial class ProductTable
    {
        [Parameter]
        public List<Product> Products { get; set; }
    }
}
