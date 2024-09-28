using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XmlToDb.Interfaces;
using XmlToDb.Models;

namespace XmlToDb.Files
{
    internal class XmlParser : IXmlParser
    {
        public List<OrderModel> Parse(string path)
        {
            XDocument xDocument = new XDocument();
            if (File.Exists(path))
                xDocument = XDocument.Load(path);
            var ordersModel = new List<OrderModel>();
            try
            {
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
            catch (Exception ex)
            {
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
    }
}
