using System;
using System.Globalization;
using System.Xml.Linq;
using XmlToDb.Models;

namespace XmlToDb
{
    public class Program
    {
        static void Main()
        {
            DbOperations dbOperations = new DbOperations();
            dbOperations.Initialize();
            Console.WriteLine("Write file path");
            string path = Console.ReadLine();
            List<OrderModel> orders = new List<OrderModel>();
            if(path is not null)
                orders = XmlParce(path);
            if(orders is not null)
            {
                dbOperations.Insert(orders);
                Print(orders);
            }
        }

        private static List<OrderModel> XmlParce(string path)
        {
            XDocument xDocument = new XDocument();
            if (File.Exists(path))
                xDocument = XDocument.Load(path);
            var ordersModel = new List<OrderModel>();
            try {
                ordersModel = xDocument.Element("orders")?.Elements("order").Select(o => new OrderModel
                {
                    Order_number = Convert.ToInt32(o.Element("order_number")?.Value),
                    Reg_date = DateOnly.Parse(o.Element("reg_date")?.Value),
                    Sum = double.Parse(o.Element("sum")?.Value, CultureInfo.InvariantCulture),
                    Products = ParceProducts(o),
                    User = ParceUser(o)                  
                }).ToList();
                return ordersModel ?? new List<OrderModel>();
            }
            catch (Exception ex) {
                throw new Exception("XML parsing error", ex);
            }
        }
        private static List<ProductModel> ParceProducts(XElement o)
        {
            return o.Elements("product")?.Select(p => new ProductModel
            {
                Quantity = Convert.ToInt32(p.Element("quantity")?.Value),
                Product_name = p.Element("product_name")?.Value,
                Price = double.Parse(p.Element("price")?.Value, CultureInfo.InvariantCulture),
            }).ToList() ?? new List<ProductModel>();
        }
        private static UserModel ParceUser(XElement o)
        {
            return o.Elements("user")?.Select(u => new UserModel
            {
                User_fio = u.Element("user_fio")?.Value,
                User_email = u.Element("user_email")?.Value,
            }).FirstOrDefault() ?? new UserModel();
        }
        private static void Print(List<OrderModel> orders)
        {
            if (orders is not null)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine($"order number: {order.Order_number}");
                    Console.WriteLine($"reg date: {order.Reg_date}");
                    Console.WriteLine($"sum: {order.Sum}\n");

                    if (order.Products is not null)
                    {
                        foreach (var product in order.Products)
                        {
                            Console.WriteLine($"quantity: {product.Quantity}");
                            Console.WriteLine($"name: {product.Product_name}");
                            Console.WriteLine($"price: {product.Price}\n");
                        }
                    }
                    if (order.User is not null)
                    {
                            Console.WriteLine($"fio: {order.User.User_fio}");
                            Console.WriteLine($"email: {order.User.User_email}\n");                   
                    }
                    Console.WriteLine('\n');
                }
            }
        }
    }
}