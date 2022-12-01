using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerSide.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
