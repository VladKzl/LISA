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
    enum AccountStatus
    {
        Ready,
        Done,
        Work,
        Off
    }
    public class LISA : ILISA// Life Imitation System Accounts for "Odnoclassniki"
    {
        // ILISA
        public string Account { get; set; }
        public DataRow AccountRow { get; set; }
        public int AccountRowNumber { get; set; }
        public Type[] RegisteredModules { get; set; }
        public int StartTimeTotalMinutes
        {
            get => (int)(DateTime.Now - (DateTime)AccountRow["session_ending"]).TotalMinutes;
        }
        // ILSAZPSettings
        public int AmountMoves { get; set; } = amountMoves;
        public int MovePause { get; set; } = movePause;
        public int SessionPause { get; set; } = sessionPause;
        public int SessionDuration { get; set; } = sessionDuaration;
        // ILISAMainCommon
        public DateTime Session_ending
        {
            get => (DateTime)AccountRow["session_ending"];
            set {
                lock (lockerDb)
                    AccountRow["session_ending"] = value;
                    NpgObjects.UpdateInner();
            }
        }
        public int Sessions_count
        {
            get => (int)AccountRow["sessions_count"];
            set {
                lock (lockerDb)
                    AccountRow["sessions_count"] =+ value;
                    NpgObjects.UpdateInner();
            }
        }
        public int Moves_count
        {
            get => (int)AccountRow["moves_count"];
            set {
                lock (lockerDb)
                    AccountRow["moves_count"] =+ value;
                    NpgObjects.UpdateInner();
            }
        }
        public string Status
        {
            get => (string)AccountRow["status"];
            set
            {
                AccountRow["status"] = value;
                NpgObjects.UpdateInner();
            }
        }
        public LISA()
        {
            lock (lockerDb)
            {
                Account = GetAccountName();
                AccountRow = Main.Select().Where(row => (string)row["profile"] == Account).First();
                Status = nameof(AccountStatus.Work);
                NpgObjects.UpdateInner();
            }
        }
        public void StartModules()
        {
            RegisterModules();
            do
            {
                try {
                    RunModule();
                }
                catch {
                    new Fatal($"{nameof(RunModule)} не выполнен.");
                }
            }
            while (CheckLimits());
        }
        private bool CheckLimits()
        {
            Moves_count++;
            if(StartTimeTotalMinutes < SessionDuration) // Session duration check
            {
                if (Moves_count < AmountMoves) // Day limit muves check
                {
                    return true;
                }
            }
            Session_ending = DateTime.Now;
            Sessions_count++;
            Status = nameof(AccountStatus.Done);

            return false; // Session Over!
        }
        private void RunModule()
        {
            Type type = RegisteredModules[new Random().Next(0, RegisteredModules.Count())];
            IModule imodule = (IModule)Activator.CreateInstance(type);
            imodule.RunModule();
        }
        private void RegisterModules()
        {
            try
            {
                RegisteredModules = (Type[])Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.Namespace == "ODDating.Modules");
            }
            catch 
            { 
                new Fatal("Не найдено ни одного модуля! " +
                "Добавьте хотя бы один модуль. Завершили работу."); 
            }
        }
        private string GetAccountName()
        {
            List<DataRow> readyAccs = GetReadyAccs();

            string accountName = null;
            List<DataRow> groupedBySessionsCount = null;
            int i = 0;
            while (accountName == null)
            {
                try
                {
                    groupedBySessionsCount = readyAccs.Where(row =>
                    Convert.ToInt32(row["sessions_count"]) == i &&
                    (DateTime.Now - (DateTime)row["session_ending"]).TotalMinutes >= sessionPause).ToList();
                }
                catch(ArgumentNullException)
                {
                    i++;
                    continue;
                }
            }
            int number = new Random().Next(0, readyAccs.Count());
            return accountName = groupedBySessionsCount.ElementAt(number)["profile"].ToString();
        }
        private List<DataRow> GetReadyAccs()
        {
            List<DataRow> readyAccs = null;
            while(readyAccs == null)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                try
                {
                    readyAccs = Main.Select().Where(row => (string)row["status"] == "Ready").ToList();
                }
                catch(ArgumentNullException) { continue; }
            }
            return readyAccs;
        }
    }
}
