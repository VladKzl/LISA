using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using MyNpg;
using System.Configuration;
using static ODDating.Program;
using static ZPBase.Base;
using Z.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ODDating
{
    public class Variables
    {
        public static JToken Json { get; set; }
        public static List<string> ActionsNames { get; set; } = new List<string>();
        public static Dictionary<string,string> ActionsStartUrls { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> MovesXpaths { get; set; } = new Dictionary<string, List<string>>();
        public Variables()
        {
            Json = JsonConvert.DeserializeObject<JToken>(Convert.ToString(project.Json));

            foreach(var name in Json["Actions"]["Names"]) // ActionsNames
                ActionsNames.Add(name.ToString());
             
            foreach (string name in ActionsNames) // ActionsStartUrls
            {
                string url = Json["Actions"]["ActionStartUrl"][name].ToString();
                ActionsStartUrls.Add(name, url);
            }
            foreach (string name in ActionsNames) // MovesXpaths
            {
                List<string> xpaths = new List<string>();
                foreach (var xpath in Json["Actions"]["MovesXpaths"][name])
                {
                    xpaths.Add(xpath.Last.ToString());
                }
                MovesXpaths.Add(name, xpaths);
            }
        }
        #region [Main]
        //Общие настройки
        public static int amountMoves;
        public static int movePause;
        public static int sessionPause;
        public static int sessionDuaration;
        public static bool DEBUGGING;
        public static string connectionStringOddating;
        public static string lisaProfilesPath;
        public static string ODDatingProfilesPath;
        #endregion
        #region [Вступление в группы]
        //Общие настройки
        public static bool groupsOn;
        public static int groupsToJoinFrom;
        public static int groupsToJoinTo;
        #endregion
        #region [Глобальные переменные]
        public static string localWarnAndInfoLogPath;
        public static string localTraceAndDebugLogPath;
        public static string localFatalAndErrorLogPath;
        public static string generalWarnAndInfoLogPath;
        public static string generalTraceAndDebugLogPath;
        public static string generalFatalAndErrorLogPath;
        #endregion
        // Локеры
        public static object lockerAppConfiguration = new object();
        public static object lockerLogMassage = new object();
        public static object lockerZennoLogMassage = new object();
        public static object lockerNlogMassage = new object();
        public static object lockerDb = new object();
        // Счетчики
        public static bool configurateAppOneTime = true;
    }
}
