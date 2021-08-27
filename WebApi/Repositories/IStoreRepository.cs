using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> Get();
        Task<Store> Get(int id);
        Task<Store> Create(Store Store);
        Task Update(Store Store);
        Task Delete(int id);
    }
}
