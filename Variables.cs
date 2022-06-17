using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using ZPBase;

namespace ODDating
{
    public static class Variables
    {
        #region [Main]
        //Общие настройки
        public static int amountMoves = Convert.ToInt32( new[] { Convert.ToInt32(Base.project.Variables["amountMovesTo"].Value), 
                                                    Convert.ToInt32(Base.project.Variables["amountMovesFrom"].Value)}.Average()); // Всего действий
        public static int movePause = Convert.ToInt32(new[] { Convert.ToInt32(Base.project.Variables["movePauseFrom"].Value),
                                                    Convert.ToInt32(Base.project.Variables["movePauseTo"].Value)}.Average()); // Пауза между действиями
        public static int sessionPause = Convert.ToInt32(new[] { Convert.ToInt32(Base.project.Variables["sessionPauseFrom"].Value),
                                                    Convert.ToInt32(Base.project.Variables["sessionPauseTo"].Value)}.Average()); // Новая сессия через
        public static int sessionDuaration = Convert.ToInt32(new[] { Convert.ToInt32(Base.project.Variables["sessionDurationFrom"].Value),
                                                 Convert.ToInt32(Base.project.Variables["sessionDurationTo"].Value)}.Average()); // Новая сессия через
        public static bool DEBUGGING = Convert.ToBoolean(Base.project.Variables["DEBUGGING"].Value);
        public static string connectionStringOddating = Base.project.Variables["connectionStringOddating"].Value; // Строка подключения к базе данных oddating
        public static string lisaProfilesPath = Base.project.Variables["lisaProfilesPath"].Value; //Аккаунты LISA OD
        public static string ODDatingProfilesPath = Base.project.Variables["ODDatingProfilesPath"].Value; // Аккаунты проекта
        #endregion
        #region [Вступление в группы]
        //Общие настройки
        public static bool groupsOn = Convert.ToBoolean(Base.project.Variables["groupsOn"].Value); //Вкл-Выкл 
        public static int groupsToJoinFrom = Convert.ToInt32(Base.project.Variables["groupsToJoinFrom"].Value); // Вступить в группы от
        public static int groupsToJoinTo = Convert.ToInt32(Base.project.Variables["groupsToJoinTo"].Value); // Вступить в группы до
        #endregion
        #region [Глобальные переменные]
        public static string localWarnAndInfoLogPath = Base.project.GlobalVariables["LogLevels", "localWarnAndInfoLogPath"].Value;
        public static string localTraceAndDebugLogPath = Base.project.GlobalVariables["LogLevels", "localTraceAndDebugLogPath"].Value;
        public static string localFatalAndErrorLogPath = Base.project.GlobalVariables["LogLevels", "localFatalAndErrorLogPath"].Value;
        public static string generalWarnAndInfoLogPath = Base.project.GlobalVariables["LogLevels", "generalWarnAndInfoLogPath"].Value;
        public static string generalTraceAndDebugLogPath = Base.project.GlobalVariables["LogLevels", "generalTraceAndDebugLogPath"].Value;
        public static string generalFatalAndErrorLogPath = Base.project.GlobalVariables["LogLevels", "generalFatalAndErrorLogPath"].Value;
        #endregion
        #region[URLs начальных страниц действий]
        public static string groupsJoinStartUrl = Base.project.Variables["groupsJoinStartUrl"].Value; // Вступить в группы до
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
