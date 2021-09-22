using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ProductMin
    {
         public string Name { get; set; }
         public decimal Price { get; set; }
    }
}