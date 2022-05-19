using System;
using System.Reflection;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ODDating.Program;
using static ODDating.Variables;

namespace ODDating.Modules
{
    class AccGeter
    {
        public string CurrentAccount { get; set; }
        public DataRow CurrentRow { get; set; }
        public int RowNumber { get; set; }

        public AccGeter()
        {
            lock (lockerAccGeter)
            {
/*                var radyAccs = Main.AsEnumerable().Where(row => (string)row["status"] == "Ready");// Пиздец!
                RowNumber = new Random().Next(1, radyAccs.Count());
                CurrentRow = radyAccs.ElementAt(RowNumber);
                CurrentAccount = CurrentRow["profile"].ToString();
                CurrentRow["status"] = "Work";
                var a = Main.Select().Where(row => row["status"].ToString() == "Work");*/
            }
        }
        public void Test()
        {
/*            var a = Assembly.GetExecutingAssembly().GetTypes();
            Type.InvokeMember
            Activator.*/
        }
    }
}
