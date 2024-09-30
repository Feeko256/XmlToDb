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
        void Create();
        void InsertOrUpdate(List<OrderModel> data);
        List<OrderModel> Return();
        void Delete();
        void Clear();
    }
}
