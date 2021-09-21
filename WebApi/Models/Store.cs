using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
       // [ForeignKey("Users")]
        public int UserId { get; set; }
       
        //public  IList<Product> Catalog { get; set; }
    }
}