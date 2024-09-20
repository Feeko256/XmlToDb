using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToDb.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string User_fio { get; set; }
        public string? User_email { get; set; }
    }
}