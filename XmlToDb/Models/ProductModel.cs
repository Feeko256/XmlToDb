﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToDb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Product_name { get; set; }
        public double Price { get; set; }
    }
}
