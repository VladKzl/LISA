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
using LogLevels;
using ODDating.Actions;
using static ZPBase.Base;
using ODDating.Entityes;
using ODDating.ProjectBase;
using static ODDating.Entityes.DBContext;

namespace LISA
{
    public class Service : Common
    {
        public Service()
        {
            lock (lockerDb)
            {
                Account = GetAccountName();
                AccountRow = Main.Select().Where(row => (string)row["profile"] == Account).First();
                AccountRow["status"] = nameof(AccountStatus.Work);
                Npg.UpdateOuter();
            }
        }
        public bool CheckLimits()
        {
            lock (lockerDb)
            {
                AccountRow["moves_count"] = +1;
                if (StartTimeTotalMinutes < SessionDuration) // Session duration check
                {
                    if ((int)AccountRow["moves_count"] < AmountMoves) // Day limit muves check
                    {
                        return true;
                    }
                }
                AccountRow["session_ending"] = DateTime.Now;
                AccountRow["sessions_count"] = +1;
                AccountRow["status"] = nameof(AccountStatus.Done);
                Npg.UpdateOuter();
            }
            return false; // Session Over!
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
                catch (ArgumentNullException)
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
            while (readyAccs == null)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.0));
                try
                {
                    readyAccs = Main.Select().Where(row =>
                    (string)row["status"] == nameof(AccountStatus.Ready)).ToList();
                }
                catch (ArgumentNullException) { continue; }
            }
            return readyAccs;
        }
    }
}
