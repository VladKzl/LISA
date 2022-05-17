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
using ODDating.LogLevels;

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
                    new Info("Получили все профили, наполняем db...");
                    foreach (string profile in (List<string>)allProfiles)
                    {
                        var row = Main.Rows.Find(profile);
                        if (row == null)
                        {
                            Main.Rows.Add(profile);
                        }
                    }
                    new Info("Закнчили наполнение db");
                }
                if (ConfigurationCache.TryGetValue("bannedProfiles", out bannedProfiles))
                {
                    new Info("Нашли забаненные профили - удаляем...");
                    foreach (string profile in (List<string>)bannedProfiles)
                    {
                        var row = Main.Rows.Find(profile);
                        if(row != null)
                        {
                            row.Delete();
                        }
                    }
                    new Info("Забаненные удалены");
                }
                if(ConfigurationCache.TryGetValue("newProfiles", out newProfiles))
                {
                    new Info("Добавляем новые профили...");
                    foreach (string profile in (List<string>)newProfiles)
                    {
                        var row = Main.Rows.Find(profile);
                        if (row == null)
                        {
                            Main.Rows.Add(profile);
                        }
                    }
                    new Info("Добавили новые профили");
                }

                NpgObjects.Adapter.Update(NpgObjects.DataSet);
            }
        }
    }
}
