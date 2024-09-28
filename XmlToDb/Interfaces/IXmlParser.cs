using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlToDb.Models;

namespace XmlToDb.Interfaces
{
    public interface IXmlParser
    {
        List<OrderModel> Parse(string path);
    }
}
