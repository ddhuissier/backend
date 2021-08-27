using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Store> Create(Store Store)
        {
            _context.Stores.Add(Store);
            await _context.SaveChangesAsync();

            return Store;
        }

        public async Task Delete(int id)
        {
            var StoreToDelete = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(StoreToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Store>> Get()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> Get(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task Update(Store Store)
        {
            _context.Entry(Store).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
