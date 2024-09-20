using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Models;

namespace XmlToDb
{
    public class ApplicationContext : DbContext
    {
        public DbSet<OrderModel> Orders { get; set; } = null;
        public DbSet<ProductModel> Products { get; set; } = null;
        public DbSet<UserModel> Users { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orders.db");
        }
    }
}
