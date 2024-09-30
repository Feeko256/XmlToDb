using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Interfaces;
using XmlToDb.Models;

namespace XmlToDb.Db
{
    public class DbOperations : IDbOperations
    {
        public void Create()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
            }
        }
        private void RemoveOrder(dynamic order)
        {
            using (var db = new ApplicationContext())
            {
                try
                {
                    if(order is not null)
                    {
                        db.Orders.RemoveRange(order);
                        db.SaveChanges();
                    }
                  
                }
                catch
                {
                    /*
                     Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
                    "The database operation was expected to affect 1 row(s),
                    but actually affected 0 row(s); data may have been modified 
                    or deleted since entities were loaded."
                     */
                }
            }
        }
        private void RemoveUser(dynamic user)
        {
            using (var db = new ApplicationContext())
            {
                if(user is not null)
                {
                    db.Users.RemoveRange(user);
                    db.SaveChanges();
                }      
            }
        }
        private void RemoveProduct(dynamic product)
        {
            using (var db = new ApplicationContext())
            {
                if(product is not null)
                {
                    db.Products.RemoveRange(product);
                    db.SaveChanges();
                }
            }
        }
        public void Clear()
        {
            using (var db = new ApplicationContext())
            {
                RemoveProduct(db.Products.ToList());
                RemoveUser(db.Users.ToList());
                RemoveOrder(db.Orders.ToList());               
            }
        }
        public void Delete()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
            }
        }
        public void InsertOrUpdate(List<OrderModel> orders)
        {
            using (var db = new ApplicationContext())
            {         
                foreach (var order in orders)
                {
                    var currentOrder = db.Orders
                        .Include(o => o.Products)
                        .Include(o => o.User)
                        .FirstOrDefault(o => o.Order_number == order.Order_number);

                    if (currentOrder is not null)
                    {
                        //update or add user
                        if (currentOrder.User.User_fio != order.User.User_fio)
                        {
                            currentOrder.User.User_fio = order.User.User_fio;
                            currentOrder.User.User_email = order.User.User_email;
                        }

                        //update or add products
                        foreach (var product in order.Products)
                        {
                            var currentProduct = currentOrder.Products
                                .FirstOrDefault(p => p.Product_name == product.Product_name);
                            if (currentProduct is not null)
                            {
                                currentProduct.Quantity = product.Quantity;
                                currentProduct.Price = product.Price;
                            }
                            else
                            {
                                currentOrder.Products.Add(product);
                            }
                        }
                        //remove products that have been deleted
                        var remProducts = currentOrder.Products.Where(p => !order.Products.Any(op => op.Product_name == p.Product_name)).ToList();
                        RemoveProduct(remProducts);
                        //update order
                        currentOrder.Sum = order.Sum;
                        currentOrder.Reg_date = order.Reg_date;
                    }
                    else
                    {
                        db.Orders.Add(order);
                    }
                }
                var ordersToRemove = db.Orders
                       .Include(o => o.Products)
                       .Include(o => o.User)
                       .ToList()
                       .Where(o => !orders.Any(newOrder => newOrder.Order_number == o.Order_number))
                       .ToList();
                foreach (var orderToRemove in ordersToRemove)
                {
                    RemoveProduct(orderToRemove.Products);
                    /*
                     in case it will be necessary to associate one user with several orders
                     */
                    // var userInOtherOrders = db.Orders.Any(o => o.User.Id == orderToRemove.User.Id && o.Order_number != orderToRemove.Order_number);
                    //if (!userInOtherOrders)
                    RemoveUser(orderToRemove.User);
                    RemoveOrder(orderToRemove);
                }
                db.SaveChanges();
            }
        }
        public List<OrderModel> Return()
        {
            var orders = new List<OrderModel>();
            using (var db = new ApplicationContext())
            {
                if(db.Database.CanConnect())
                    orders = db.Orders.Include(o => o.User).Include(p => p.Products).ToList();
            }
            return orders ?? new List<OrderModel>();
        }
    }
}