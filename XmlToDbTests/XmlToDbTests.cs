using XmlToDb.Db;
using XmlToDb.Files;
using XmlToDb.Interfaces;
using XmlToDb.Models;

namespace XmlToDbTests
{
    public class XmlParserTest
    {
        private readonly XmlParser _parser;
        private static List<OrderModel> _expectedOrders;

        public XmlParserTest()
        {
            _parser = new XmlParser();

            _expectedOrders = new List<OrderModel>
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
                },
                new OrderModel
                {
                    Order_number = 2,
                    Sum = 45780.48,
                    Reg_date = new DateOnly(2022, 10, 14),
                    Products = new List<ProductModel>
                    {
                        new ProductModel
                        {
                            Quantity = 5,
                            Product_name = "Sony",
                            Price = 40567.24
                        },
                        new ProductModel
                        {
                            Quantity = 2,
                            Product_name = "Gamepad",
                            Price = 5213.24
                        }
                    },
                    User = new UserModel
                    {
                        User_fio = "Петров Петр Петрович",
                        User_email = "dce@email.com"
                    }
                }
            };
        }
        [Theory]
        [InlineData("D:\\YandexDisk\\YandexDisk\\XmlToDb\\XmlToDbTests\\TestOrders.xml")]
        public void Parse_CanParceDataFromXmlFile(string path)
        {
            var result = _parser.Parse(path);
            for (int i = 0; i < _expectedOrders.Count; i++)
            {
                Assert.Equal(_expectedOrders[i].Order_number, result[i].Order_number);
                Assert.Equal(_expectedOrders[i].Reg_date, result[i].Reg_date);
                Assert.Equal(_expectedOrders[i].Sum, result[i].Sum);
                for(int j = 0; j < _expectedOrders[i].Products.Count; j++)
                {
                    Assert.Equal(_expectedOrders[i].Products[j].Quantity,
                        result[i].Products[j].Quantity);
                    Assert.Equal(_expectedOrders[i].Products[j].Product_name,
                        result[i].Products[j].Product_name);
                    Assert.Equal(_expectedOrders[i].Products[j].Price,
                        result[i].Products[j].Price);
                }
                Assert.Equal(_expectedOrders[i].User.User_fio, result[i].User.User_fio);
                Assert.Equal(_expectedOrders[i].User.User_email, result[i].User.User_email);
            }
        }
    }
}