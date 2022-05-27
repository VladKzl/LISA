using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ODDating.Program;
using static ODDating.Variables;
using System.Threading;

namespace ODDating.ModulesControl
{
    public class LISA // Life Imitation System Accounts for "Odnoclassniki"
    {
        public string Account { get; set; }
        public DataRow AccountRow { get; set; }
        public int AccountRowNumber { get; set; }
        private Type[] RegisteredModules { get; set; }
        public LISA()
        {
            lock (lockerDbSerch)
            {
                Account = GetAccountName();

                AccountRowNumber = new Random().Next(0, readyAccs.Count());
                AccountRow = readyAccs.ElementAt(AccountRowNumber);
                Account = AccountRow["profile"].ToString();
                AccountRow["status"] = "Work";
            }
        }
        public void StartModules()
        {

        }
        private void RegisterModules()
        {
            Type[] modules = (Type[])Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Namespace == "ODDating.Modules");
            RegisteredModules = modules;
            /*            Type.InvokeMember
                        Activator.*/
        }
        private string GetAccountName()
        {
            IEnumerable<DataRow> readyAccs;
            do
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                readyAccs = Main.Select().Where(row => (string)row["status"] == "ldal");//Ready
            }
            while (readyAccs.Count() == 0);

            string accountName = null;
            for(int i = 0; i < int.MaxValue; i++)
            {
                IEnumerable<DataRow> readyAccsSortedBySessionsCount = readyAccs.Where(row => Convert.ToInt32(row["sessions_count"]) == i);
                if (readyAccsSortedBySessionsCount.Count() != 0)
                {
                    int number = new Random().Next(0, readyAccs.Count());
                    accountName = readyAccsSortedBySessionsCount.ElementAt(number)["profile"].ToString();
                }
                while (accountName == null) ;
            }
            while ();
            return accountName;
        }
    }
}
