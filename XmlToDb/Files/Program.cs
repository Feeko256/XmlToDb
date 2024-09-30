using System;
using System.Globalization;
using System.Xml.Linq;
using XmlToDb.Db;
using XmlToDb.Interfaces;
using XmlToDb.Models;

namespace XmlToDb.Files
{
    public class Program
    {
        private IDbOperations _dbOperations;
        private IXmlParser _xmlParser;
        private IPrint _Print;
        public Program(IDbOperations dbOperations, IXmlParser xmlParser, IPrint print)
        {
            _dbOperations = dbOperations;
            _xmlParser = xmlParser;
            _Print = print;
        }
        static void Main()
        {
            var dbOperations = new DbOperations();
            var xmlParser = new XmlParser();
            var print = new Print();
            var program = new Program(dbOperations, xmlParser, print);

            program.Start();
        }
        public void Start()
        {
            //_dbOperations.Delete();
            _dbOperations.Create();
            //_dbOperations.Clear();
            

            Console.WriteLine("Write file path");
            var path = Console.ReadLine();
            var orders = new List<OrderModel>();
            if (path is not null)
                orders = _xmlParser.Parse(path);
            if (orders is not null)
                _dbOperations.InsertOrUpdate(orders);

            _Print.PrintToConsol(_dbOperations.Return());
        }
    }
}