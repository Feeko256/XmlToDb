using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Models;

namespace XmlToDb.Interfaces
{
    public interface IDbOperations
    {
        void Initialize();
        void Insert(List<OrderModel> orders);
    }
}
