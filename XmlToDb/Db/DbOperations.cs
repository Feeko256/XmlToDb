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
        public void Initialize()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
        public void Insert(List<OrderModel> orders)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.AddRange(orders);
                db.SaveChanges();
            }
        }
    }
}
