using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Interfaces;

namespace XmlToDb.Files
{
    public class Print : IPrint
    {
        public void PrintToConsol(dynamic orders)
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
