using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {
           Database.EnsureCreated();
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMin> ProductMinimumValues { get; set; }
        public DbSet<ProductSp> ProductStoreProck { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().Property(m => m.Price).HasPrecision(10, 2);
            //builder.Entity<ProductMin>().HasNoKey();
            // Get view data
            builder
                .Entity<ProductMin>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("v_Products_min_values");
                    eb.Property(v => v.Name).HasColumnName("Name"); // Use if different model != sql field name
                });
            // Get product by store prock
            builder
               .Entity<ProductSp>(
               eb =>
               {
                   eb.HasNoKey();
               });
        }
    }
}
