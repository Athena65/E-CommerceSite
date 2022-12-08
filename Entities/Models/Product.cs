using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name is required Pls Type a Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Name is required Pls Type a Supplier")]
        public string? Supplier { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Price Value Can't be Lower than 1$")]
        public int Price { get; set; }
        public string? ImageUrl { get; set; }

       
    }
}