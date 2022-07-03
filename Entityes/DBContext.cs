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
        public static DataSet DataSet { get; set; } = new DataSet();
        public static DataTable Main { get; set; } = new DataTable();
        public static DataTable Groups { get; set; } = new DataTable();
        public static DataTable GroupsStatistics { get; set; } = new DataTable();
        

    } 
}
