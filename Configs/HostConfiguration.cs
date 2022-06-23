using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ODDating;
using static ODDating.Program;
using static ODDating.Variables;
using static ODDating.Entityes.DBContext;

namespace ODDating.Configs
{
    class HostConfiguration
    {
        public static void FillProjectProfilesDirectory()
        {
            List<string> lisaProfiles = new List<string>(Directory.GetFiles(lisaProfilesPath));
            List<string> projectProfiles = new List<string>(Directory.GetFiles(ODDatingProfilesPath));

            if (projectProfiles.Count == 0)
            {
                List<string> profiles = new List<string>();

                foreach (string profile in lisaProfiles)
                {
                    File.Copy(profile, Path.Combine(ODDatingProfilesPath, Regex.Match(profile, @"(?<=Profiles\\).*").Value));
                    if (IsProfileExistInDb(profile))
                    {
                        /*profiles.Add(Regex.Match(profile, @"(?<=Profiles\\).*").Value);*/
                        profiles.Add(profile);
                    }
                }
                AppCachesCollection.ConfigurationCache.Set("allProfiles", profiles);
            }
            else 
            {
                List<string> bannedProfiles = new List<string>();
                foreach (string profile in projectProfiles)
                {
                    string projectProfile = Path.Combine(lisaProfilesPath, Regex.Match(profile, @"(?<=Profiles\\).*").Value);
                    if (!lisaProfiles.Exists(x => x.Equals(projectProfile)))
                    {
                        File.Delete(profile);
                        if (IsProfileExistInDb(profile))
                        {
                            bannedProfiles.Add(profile);
                        }
                    }
                    AppCachesCollection.ConfigurationCache.Set("bannedProfiles", bannedProfiles);
                }
                List<string> newProfiles = new List<string>();
                foreach (string profile in lisaProfiles)
                {
                    string newPath = Path.Combine(ODDatingProfilesPath, Regex.Match(profile, @"(?<=Profiles\\).*").Value);
                    if (!File.Exists(newPath))
                    {
                        File.Copy(profile, newPath);
                        if (!IsProfileExistInDb(profile))
                        {
                            newProfiles.Add(profile);
                        }
                    }
                    AppCachesCollection.ConfigurationCache.Set("newProfiles", newProfiles);
                }
            }
            if (Main.Rows.Count == 0)
            {
                foreach(string profile in projectProfiles)
                {
                    Main.Rows.Add(Regex.Match(profile, @"(?<=Profiles\\).*").Value);
                }
            }
        }
        private static bool IsProfileExistInDb(string profile)
        {
            string profileShort = Regex.Match(profile, @"(?<=Profiles\\).*").Value;
            if (Main.Rows.Find(profileShort) == null)
                return false;
            return true;
        }
    }
}
