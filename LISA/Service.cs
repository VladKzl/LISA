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
using System.Text.RegularExpressions;

namespace LISA
{
    public class Service : Common
    {
        public Service()
        {
            lock (lockerDb)
            {
                AccountPath = GetAccountPath();
                AccountName = Regex.Match(AccountPath, @"(?<=Profiles\\).*").Value;
                AccountRow = Main.Select().Where(row => (string)row["profile"] == AccountName).First();

                AccountRow["status"] = nameof(AccountStatus.Work);
                Npg.UpdateOuter();
            }
        }
        public bool CheckLimits()
        {
            lock (lockerDb)
            {
                AccountRow["moves_count"] = +1;
                if (StartTimeTotalMinutes.Value < SessionDuration) // Session duration check
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
        private string GetAccountPath()
        {
            List<DataRow> readyAccs = GetReadyAccs();

            string accountPath = null;
            List<DataRow> groupedBySessionsCount = null;
            int i = 0;
            while (accountPath == null)
            {
                try
                {
                    groupedBySessionsCount = readyAccs.Where(row =>
                    Convert.ToInt32(row["sessions_count"]) == i &&
                    (DateTime.Now - (DateTime)row["session_ending"]).TotalMinutes >= sessionPause).ToList();

                    int number = new Random().Next(0, groupedBySessionsCount.Count());
                    accountPath = groupedBySessionsCount.ElementAt(number)["path"].ToString();
                }
                catch (ArgumentNullException)
                {
                    i++;
                    continue;
                }
            }
            return accountPath;
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
