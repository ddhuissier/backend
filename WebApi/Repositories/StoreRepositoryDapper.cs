using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class StoreRepositoryDapper : IStoreRepository
    {
        private IDbConnection db;
        public StoreRepositoryDapper(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetValue<string>("SqlBD:ConnectionString"));
        }

        public async Task<Store> Create(Store store)
        {
            var sql = "INSERT INTO STORES (Id,Name,Activity,UserId) VALUES  (@Id,@Name,@Activity,@UserId)"
            + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await db.QueryAsync<int>(sql, new
            {
                @Name = store.Name,
                @Activity = store.Activity,
                @UserId = store.UserId
            });
            store.Id = id.Single();
            // Or
            //var id = await db.ExecuteScalarAsync<int>(sql, store);
            //store.Id = id;

            return store;
        }

        public async Task Delete(int id)
        {
            var sql = "DELETE FROM STORES WHERE Id= @Id";
            await db.ExecuteAsync(sql, new { @Id = id});
        }

        public async Task<IEnumerable<Store>> Get()
        {
            var sql = "SELECT * from STORES";
            return await db.QueryAsync<Store>(sql);
        }

        public async Task<Store> Get(int id)
        {
            var sql = "SELECT * from STORES where Id = @Id";
            return await db.QueryFirstAsync<Store>(sql, new { @Id = id });
        }

        public async Task Update(Store store)
        {
            var sql = "UPDATE STORES  SET Id = @Id ,Name = @Name, Activity = @Activity, UserId = @UserId WHERE Id= @Id";
            await db.ExecuteAsync(sql, store);
        }
    }
}
