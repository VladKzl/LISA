using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using static ODDating.Program;
using static ODDating.AppCachesCollection;
using static ODDating.Variables;
using LogLevels;
using static ODDating.Entityes.DBContext;

namespace ODDating.Configs
{
    public class DBConfifuration
    {
        public class OddatingMain
        {
            static public void RefreshProfileColumn()
            {
                object allProfiles = null;
                object bannedProfiles = null;
                object newProfiles = null;
                if (ConfigurationCache.TryGetValue("allProfiles", out allProfiles))
                {
                    foreach (string profile in (List<string>)allProfiles)
                    {
                        Main.Rows.Add(profile, Regex.Match(profile, @"(?<=Profiles\\).*").Value);
                    }
                }
                if (ConfigurationCache.TryGetValue("bannedProfiles", out bannedProfiles))
                {
                    foreach (string profile in (List<string>)bannedProfiles)
                    {
                        Main.Rows.Find(Regex.Match(profile, @"(?<=Profiles\\).*").Value).Delete();
                    }
                }
                if(ConfigurationCache.TryGetValue("newProfiles", out newProfiles))
                {
                    foreach (string profile in (List<string>)newProfiles)
                    {
                        Main.Rows.Add(profile);
                    }
                }
            }
            static public void NewDayRefreshColumns()
            {
                foreach (DataRow row in Main.Rows)
                {
                    if(DateTime.Today.Day != ((DateTime)row["session_ending"]).Day)
                    {
                        row["session_ending"] = DateTime.Now;
                        row["sessions_count"] = 0;
                        row["moves_count"] = 0;
                    }
                }
            }
        }
    }
}
