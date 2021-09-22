using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product Product)
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return Product;
        }

        public async Task Delete(int id)
        {
            var ProductToDelete = await _context.Products.FindAsync(id);
            _context.Products.Remove(ProductToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> Get()
        {
            // test get view product min values 
            var prodMinValues = _context.ProductMinimumValues.ToList();

            foreach (var pmv in prodMinValues)
            {
                Console.WriteLine($"{pmv.Name} has {pmv.Price} like price.");
                Console.WriteLine();
            }

            // Test call [dbo].[spGetProductById]
            var param = new SqlParameter("@ID", "1");
            var prod1 = _context.ProductStoreProck
                       .FromSqlRaw("exec spGetProductById @ID", param)
                       .AsEnumerable()
                      .FirstOrDefault();
                     
            Console.WriteLine($"{prod1.Name} has {prod1.Price} like price.");

            return await _context.Products.AsNoTracking().ToListAsync(); // No-tracking changes for get query
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        
        public async Task Update(Product Product)
        {
            _context.Entry(Product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
