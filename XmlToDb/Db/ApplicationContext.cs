using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Models;

namespace XmlToDb.Db
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; } = null;
        public DbSet<ProductModel> Products { get; set; } = null;
        public DbSet<UserModel> Users { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Products)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
