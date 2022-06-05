using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static ODDating.Program;
using static ODDating.Variables;
using ODDating.Interfaces;
using ODDating.LogLevels;

namespace ODDating.ModulesControl
{
    public class LISA : ILISA// Life Imitation System Accounts for "Odnoclassniki"
    {
        public string Account { get; set; }
        public DataRow AccountRow { get; set; }
        public int AccountRowNumber { get; set; }
        public Type[] RegisteredModules { get; set; }
        public int AmountMoves { get; set; } = amountMoves;
        public int MovePause { get; set; } = movePause;
        public int SessionPause { get; set; } = sessionPause;
        public LISA()
        {
            lock (lockerDb)
            {
                Account = GetAccountName();
                AccountRow = Main.Select().Where(row => (string)row["profile"] == Account).First();
                AccountRow["status"] = "Work";
                NpgObjects.UpdateInner();
            }
        }
        public void StartModules()
        {
            RegisterModules();
            int moves_count;
            do
            {
                moves_count = RunModule();
            }
            while (moves_count < AmountMoves);
        }
        private int RunModule()
        {
            Type type = RegisteredModules[new Random().Next(0, RegisteredModules.Count())];
            IModule imodule = (IModule)Activator.CreateInstance(type);
            return imodule.RunModule();
        }
        private void RegisterModules()
        {
            try
            {
                Type[] modules = (Type[])Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.Namespace == "ODDating.Modules");
                RegisteredModules = modules;
            }
            catch { new Fatal("Не найдено ни одного модуля! " +
                "Добавьте хотя бы один модуль. Завершили работу."); 
            }
        }
        public delegate bool SessionCheck();
        private string GetAccountName()
        {
            List<DataRow> readyAccs = GetReadyAccs();
/*            DateTime sessiont = (DateTime)readyAccs[2]["session_ending"];
            DateTime now = DateTime.Now;
            TimeSpan b = now - sessiont;
            string c = b.ToString();
            TimeSpan d = TimeSpan.Parse(c);
            int e = (int)d.TotalMinutes;*/

            string accountName = null;
            int i = 0;
            do
            {
                List<DataRow> groupedBySessionsCount = readyAccs.Where(row =>
                Convert.ToInt32(row["sessions_count"]) == i &&
                (DateTime.Now - (DateTime)row["session_ending"]).TotalMinutes >= sessionPause).ToList();
                if (groupedBySessionsCount.Count() != 0)
                {
                    int number = new Random().Next(0, readyAccs.Count());
                    accountName = groupedBySessionsCount.ElementAt(number)["profile"].ToString();
                }
                i++;
            }
            while (accountName == null);
            return accountName;
        }
        private List<DataRow> GetReadyAccs()
        {
            List<DataRow> readyAccs;
            do
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                readyAccs = Main.Select().Where(row => (string)row["status"] == "Ready").ToList();
            }
            while (readyAccs.Count() == 0);
            return readyAccs;
        }
    }
}
