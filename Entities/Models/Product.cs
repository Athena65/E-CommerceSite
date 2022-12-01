namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }

       
    }
}