using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODDating.Entityes
{
    public static class DBContext
    {
        public static DataSet DataSet { get; set; }
        public static DataTable Main { get; set; }
        public static DataTable Groups { get; set; }
    } 
}
