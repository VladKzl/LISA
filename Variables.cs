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
using ODDating;

namespace ODDating
{
    public static class Variables
    {

        #region [Main]
        //Общие настройки
        public static int amountMoves; // Всего действий
        public static int movePause; // Пауза между действиями
        public static int sessionPause; // Новая сессия через
        public static int sessionDuaration; // Время сессии
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
