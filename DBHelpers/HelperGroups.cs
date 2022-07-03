using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LISA.Common;
using static ODDating.Entityes.DBContext;
using ODDating;
using static ODDating.Program;
using Npgsql;

namespace ODDating.DBHelpers
{
    public class HelperGroups
    {
        [ThreadStatic]
        public static DataRow GroupRow;
        [ThreadStatic]
        public static int GroupId;
        [ThreadStatic]
        public static string GroupUrl;
        [ThreadStatic]
        public static string GroupName;
        [ThreadStatic]
        public static string GroupShortName;
        [ThreadStatic]
        public static string GroupInfo;
        public static List<string> GroupsUrls { get; set; }
        string AccountName { get; set; } = "main.zpprofile";
        public HelperGroups()
        {
            GroupsUrls = Groups.Select().Select(x => x["url"].ToString()).ToList();
            GroupRow = GetGroupsRow();
            GroupId = (int)GroupRow["id"];
            GroupUrl = (string)GroupRow["url"];
            GroupName = (string)GroupRow["name"];
            GroupShortName = (string)GroupRow["short_name"];
            GroupInfo = (string)GroupRow["url"];
        }
        private DataRow GetGroupsRow()
        {
            List<string> usedProfileGroupsUrls = new List<string>();
            DataRow groupsStatisticsRow = GroupsStatistics.Select().Where(x => (string)x["profile"] == AccountName).First();
            if (((Array)groupsStatisticsRow["groups"]).Length != 0)
            {
                foreach (string group in (Array)groupsStatisticsRow["groups"])
                {
                    usedProfileGroupsUrls.Add(group);
                }
                List<string> exept = GroupsUrls.Except(usedProfileGroupsUrls).ToList();
                string url = exept[new Random().Next(0, exept.Count())];
                DataRow row = Groups.Select().Where(x => (string)x["url"] == url).First();
                return row;
            }
            return Groups.Select().First();
        }
        public static void AddToUsedGroups()
        {
            Npg.Connection.Open();
            NpgsqlCommand command = new NpgsqlCommand() { CommandText = "SELECT add_to_used_group('test11','main.zpprofile')", Connection = Npg.Connection };
            command.ExecuteNonQuery();
            DataRow groupsStatisticsRow = GroupsStatistics.Select().Where(x => (string)x["profile"] == "main.zpprofile").First();
            Npg.UpdateOuter();
        } 
    }
}
