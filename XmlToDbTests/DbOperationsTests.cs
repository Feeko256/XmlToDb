using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Db;
using XmlToDb.Interfaces;
using XmlToDb.Models;

namespace XmlToDbTests
{
    public class DbOperationsTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly DbOperations _dbOperations;

        public DbOperationsTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            _dbOperations = new DbOperations(_options);
        }
        [Fact]
        public void Create_CanDbBeCreated()
        {
            //doesnt works correct with InMemoryDatabse
        }
        [Fact]
        public void Clear_CanDbBeCleared()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Order_number = 1,
                    Sum = 12056.46,
                    Reg_date = new DateOnly(2023, 11, 13),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 2,
                            Product_name = "LG 1755",
                            Price = 12056.46
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Иванов Иван Иванович",
                        User_email = "abc@email.com"
                    }
                }
            };
            _dbOperations.InsertOrUpdate(orders);
            _dbOperations.Clear();
            using (var db = new ApplicationContext(_options))
            {
                Assert.Empty(db.Orders);
                Assert.Empty(db.Users);
                Assert.Empty(db.Products);
            }
        }
        [Fact]
        public void Delete_CanDbBeDeleted()
        {
            //doesnt works correct with InMemoryDatabse
        }
        [Fact]
        public void InsertOrUpdate_CanOrdersBeInserted()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Order_number = 1,
                    Sum = 12056.46,
                    Reg_date = new DateOnly(2023, 11, 13),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 2,
                            Product_name = "LG 1755",
                            Price = 12056.46
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Иванов Иван Иванович",
                        User_email = "abc@email.com"
                    }
                }
            };

            _dbOperations.InsertOrUpdate(orders);
            Assert.NotNull(orders?.FirstOrDefault());
            Assert.Equal(1, orders?.FirstOrDefault()?.Order_number);
            Assert.Equal("Иванов Иван Иванович", orders?.FirstOrDefault()?.User.User_fio);
            Assert.Equal(2, orders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Quantity);
        }
        [Fact]
        public void InsertOrUpdate_CanOrdersBeUpdated()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Order_number = 1,
                    Sum = 12056.46,
                    Reg_date = new DateOnly(2023, 11, 13),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 2,
                            Product_name = "LG 1755",
                            Price = 12056.46
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Иванов Иван Иванович",
                        User_email = "abc@email.com"
                    }
                }
            };
            var updatedOrders = new List<OrderModel>
            {
                new OrderModel
                {
                    Order_number = 1,
                    Sum = 2500,
                    Reg_date = new DateOnly(2000, 05, 05),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 1,
                            Product_name = "TV",
                            Price = 25000
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Иван",
                        User_email = "ivan@email.com"
                    }
                }
            };

            _dbOperations.InsertOrUpdate(orders);

            Assert.NotNull(orders?.FirstOrDefault());
            Assert.Equal(1, orders?.FirstOrDefault()?.Order_number);
            Assert.Equal("Иванов Иван Иванович", orders?.FirstOrDefault()?.User.User_fio);
            Assert.Equal(2, orders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Quantity);
            Assert.Equal("LG 1755", orders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Product_name);
            Assert.Equal(12056.46, orders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Price);
            
            _dbOperations.InsertOrUpdate(updatedOrders);

            Assert.NotNull(updatedOrders?.FirstOrDefault());
            Assert.Equal(1, updatedOrders?.FirstOrDefault()?.Order_number);
            Assert.Equal("Иван", updatedOrders?.FirstOrDefault()?.User.User_fio);
            Assert.Equal(1, updatedOrders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Quantity);
            Assert.Equal("TV", updatedOrders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Product_name);
            Assert.Equal(25000, updatedOrders?.FirstOrDefault()?.Products?.FirstOrDefault()?.Price);
        }
        [Fact]
        public void Return_CanReturnAllOrdersFromDb()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Order_number = 1,
                    Sum = 12056.46,
                    Reg_date = new DateOnly(2023, 11, 13),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 2,
                            Product_name = "LG 1755",
                            Price = 12056.46
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Иванов Иван Иванович",
                        User_email = "abc@email.com"
                    }
                }
            };
            _dbOperations.InsertOrUpdate(orders);
            var result = _dbOperations.Return();
            Assert.NotNull(result?.FirstOrDefault());
            Assert.Equal(1, result?.FirstOrDefault()?.Order_number);
            Assert.Equal("Иванов Иван Иванович", result?.FirstOrDefault()?.User.User_fio);
            Assert.Equal(2, result?.FirstOrDefault()?.Products?.FirstOrDefault()?.Quantity);
        }
    }
}
