using System.Collections.Generic;

namespace WebApi.Models
{
    public class Store
    {
        public Store()
        {
            //Catalog = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public int UserId { get; set; }
        

       // public virtual ICollection<Product> Catalog { get; set; }
    }
}