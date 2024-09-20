using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToDb.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int Order_number { get; set; }
        public double Sum { get; set; }
        public DateOnly Reg_date { get; set; }
        public List<ProductModel> Products { get; set; }
        public UserModel User { get; set; }
    }
}
