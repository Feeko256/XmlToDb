using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Db;
using XmlToDb.Models;

namespace XmlToDb.Db
{
    public class TestApplicationContext : DbContext
    {
        public TestApplicationContext(DbContextOptions<TestApplicationContext> options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; } = null;
        public DbSet<ProductModel> Products { get; set; } = null;
        public DbSet<UserModel> Users { get; set; } = null;

    }
}
