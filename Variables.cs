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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ODDating
{
    public class Variables
    {
        #region [Json]
        public static JToken Json { get; set; }
        public static List<string> ActionsNames { get; set; } = new List<string>();
        public static Dictionary<string,string> ActionsStartUrls { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> MovesXpaths { get; set; } = new Dictionary<string, List<string>>();
        #endregion
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
        #region [Локеры]
        public static object lockerAppConfiguration = new object();
        public static object lockerLogMassage = new object();
        public static object lockerZennoLogMassage = new object();
        public static object lockerNlogMassage = new object();
        public static object lockerDb = new object();
        #endregion
        #region [Счетчики]
        public static bool configurateAppOneTime = true;
        #endregion
        public Variables(Instance instance, IZennoPosterProjectModel project)
        {
            InitializeJson();
            InitializeVariables();
        }
        private void InitializeJson()
        {
            Json = JsonConvert.DeserializeObject<JToken>(Convert.ToString(project.Json));

            foreach (var name in Json["Actions"]["Names"]) // ActionsNames
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
        private void InitializeVariables()
        {
            #region [Main]
            //Общие настройки
            amountMoves = Convert.ToInt32(new[] { Convert.ToInt32(project.Variables["amountMovesTo"].Value),
                                    Convert.ToInt32(project.Variables["amountMovesFrom"].Value)}.Average()); // Всего действий
            movePause = Convert.ToInt32(new[] { Convert.ToInt32(project.Variables["movePauseFrom"].Value),
                                    Convert.ToInt32(project.Variables["movePauseTo"].Value)}.Average()); // Пауза между действиями
            sessionPause = Convert.ToInt32(new[] { Convert.ToInt32(project.Variables["sessionPauseFrom"].Value),
                                    Convert.ToInt32(project.Variables["sessionPauseTo"].Value)}.Average()); // Новая сессия через
            sessionDuaration = Convert.ToInt32(new[] { Convert.ToInt32(project.Variables["sessionDurationFrom"].Value),
                                    Convert.ToInt32(project.Variables["sessionDurationTo"].Value)}.Average()); // Новая сессия через
            DEBUGGING = Convert.ToBoolean(project.Variables["DEBUGGING"].Value);
            //DB
            connectionStringOddating = project.Variables["connectionStringOddating"].Value; // Строка подключения к базе данных oddating
            //Директории
            lisaProfilesPath = project.Variables["lisaProfilesPath"].Value; //Аккаунты LISA OD
            ODDatingProfilesPath = project.Variables["ODDatingProfilesPath"].Value; // Аккаунты проекта
            #endregion
            #region [Вступление в группы]
            //Общие настройки
            groupsOn = Convert.ToBoolean(project.Variables["groupsOn"].Value); //Вкл-Выкл 
            groupsToJoinFrom = Convert.ToInt32(project.Variables["groupsToJoinFrom"].Value); // Вступить в группы от
            groupsToJoinTo = Convert.ToInt32(project.Variables["groupsToJoinTo"].Value); // Вступить в группы до
            #endregion
            #region [Глобальные переменные]
            localWarnAndInfoLogPath = project.GlobalVariables["LogLevels", "localWarnAndInfoLogPath"].Value;
            localTraceAndDebugLogPath = project.GlobalVariables["LogLevels", "localTraceAndDebugLogPath"].Value;
            localFatalAndErrorLogPath = project.GlobalVariables["LogLevels", "localFatalAndErrorLogPath"].Value;
            generalWarnAndInfoLogPath = project.GlobalVariables["LogLevels", "generalWarnAndInfoLogPath"].Value;
            generalTraceAndDebugLogPath = project.GlobalVariables["LogLevels", "generalTraceAndDebugLogPath"].Value;
            generalFatalAndErrorLogPath = project.GlobalVariables["LogLevels", "generalFatalAndErrorLogPath"].Value;
            #endregion
        }
    }
}
