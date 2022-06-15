using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNpg
{
    public static class StaticNpg
    {
        public static DataSet DataSet { get; set; }
        public static DataTable Main { get; set; }
        public static DataTable Groups { get; set; }
    } 
}
