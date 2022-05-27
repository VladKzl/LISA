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
using ODDating.MyNpg;
using System.Configuration;
using ODDating;

namespace ODDating
{
    public static class Variables
    {

        #region [Main]
        //Общие настройки
        public static int amountMovesFrom; // Всего действий от
        public static int amountMovesTo; // Всего действий до
        public static int movePauseFrom; // Пауза между действиями от
        public static int movePauseTo; // Пауза между действиями до
        public static int sessionPauseFrom; // Новая сессия через от
        public static int sessionPauseTo; // Новая сессия через до
        public static bool DEBUGGING; // Режим дебага
        //DB
        public static string connectionStringOddating; //Строка подключения к базе данных oddating
        //Директории
        public static string lisaProfilesPath; //Аккаунты LISA OD
        public static string ODDatingProfilesPath; // Аккаунты проекта
        #endregion
        #region [Вступление в группы]
        //Общие настройки
        public static bool groupsOn; //Вкл-Выкл
        public static int groupsToJoinFrom; // Вступить в группы от
        public static int groupsToJoinTo; // Вступить в группы до
        #endregion
        #region [Глобальные переменные]
        //Лог
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
