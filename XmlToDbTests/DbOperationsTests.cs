using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Db;
using XmlToDb.Interfaces;

namespace XmlToDbTests
{
    public class DbOperationsTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly DbOperations _dbOperations;

        public DbOperationsTests()
        {
            _dbOperations = new DbOperations();
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite("Data Source=orders.db");

            using (var db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
            }
        }
        [Fact]
        public void Create_CanDbBeCreated()
        {
            _dbOperations.Create();
            using (var db = new ApplicationContext())
            {
                Assert.True(db.Database.CanConnect());
            }
        }
        [Fact]
        public void Clear_CanDbBeCleared()
        {

        }
        [Fact]
        public void Delete_CanDbBeDeleted()
        {

        }
        [Fact]
        public void InsertOrUpdate_CanOrderBeInserted()
        {

        }
        [Fact]
        public void InsertOrUpdate_CanOrderBeUpdated()
        {

        }
        [Fact]
        public void Return_CanReturnAllOrdersFromDb()
        {

        }
    }
}
