using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Models;

namespace XmlToDb.Interfaces
{
    public interface IPrint
    {
        public void PrintToConsol(List<OrderModel> data);   
    }
}