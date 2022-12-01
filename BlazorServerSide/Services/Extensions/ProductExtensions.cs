using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
namespace BlazorServerSide.Services.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Search(this IQueryable<Product> products, string key) 
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                return products;
            }
            var lowerKey=key.Trim().ToLower();
            return products.Where(x=>x.Name.ToLower().Contains(lowerKey));  
        }
        public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Name);

            var orderParamas= orderByQueryString.Trim().Split(',');
            var propertyInfos= typeof(Product).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParamas)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(x => x.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if(objectProperty == null)
                {
                    continue;
                }
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");

            }
            var orderQuery= orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if(string.IsNullOrWhiteSpace(orderQuery))
            {
                return products.OrderBy(e=>e.Name);
            }
            return products.OrderBy(orderQuery);

        }
    }
}
