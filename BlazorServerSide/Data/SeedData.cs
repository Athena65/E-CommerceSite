using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorServerSide.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                if(context == null ||context.Products==null ) 
                {
                    throw new ArgumentNullException("Null DataContext");
                }
                if(context.Products.Any() )
                {
                    return;
                }
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Watson Filter",
                        Supplier = "Burak Tamince",
                        Price = 40,
                        ImageUrl = " C:\\Users\\burak\\OneDrive\\Masaüstü\\C#\\e-commerce\\filitre.jfif"
                    },
                     new Product
                     {
                         Name = "WATSON Gray Tobacco(1 Kilo)",
                         Supplier = "Burak Tamince",
                         Price = 560,
                         ImageUrl = " C:\\Users\\burak\\OneDrive\\Masaüstü\\C#\\e-commerce\\tutun.jfif"
                     },
                    new Product
                    {
                        Name = "Kefo Coal(1 Kilo)",
                        Supplier = "Burak Tamince",
                        Price = 95,
                        ImageUrl = "C:\\Users\\burak\\OneDrive\\Masaüstü\\C#\\e-commerce\\kefo komur.jpg"
                    },
                     new Product
                     {
                         Name = "WATSON Wrapping Paper(1 Piece)",
                         Supplier = "Burak Tamince",
                         Price = 3,
                         ImageUrl = " C:\\Users\\burak\\OneDrive\\Masaüstü\\C#\\e-commerce\\sarma kagidi.jpg"
                     }
                     );
                context.SaveChanges();



            }
        }
    }
}
