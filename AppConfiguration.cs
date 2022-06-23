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
using System.Configuration;
using static ZPBase.Base;
using MyNpg;
using ODDating.Configs;
using Microsoft.Extensions.Caching.Memory;
using ODDating.Interfaces;
using ODDating.Entityes;

namespace ODDating
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(Instance inst, IZennoPosterProjectModel proj)
        {
            ZPBaseConfiguration(inst, proj);
            ConfigurateVariables(inst, proj);
            ConfigurateProgram(inst, proj);
            ConfigurateAppCahcesCollection();
            ConfigurateHost();
            ConfigurateDB();
        }
        public void ZPBaseConfiguration(Instance inst, IZennoPosterProjectModel proj)
        {
            project = proj;
            instance = inst;
        }
        public void ConfigurateVariables(Instance inst, IZennoPosterProjectModel proj)
        {
            new Variables();
            #region [Main]
            //Общие настройки
            Variables.amountMoves = Convert.ToInt32( new[] { Convert.ToInt32(proj.Variables["amountMovesTo"].Value), 
                                    Convert.ToInt32(proj.Variables["amountMovesFrom"].Value)}.Average()); // Всего действий
            Variables.movePause = Convert.ToInt32(new[] { Convert.ToInt32(proj.Variables["movePauseFrom"].Value),
                                    Convert.ToInt32(proj.Variables["movePauseTo"].Value)}.Average()); // Пауза между действиями
            Variables.sessionPause = Convert.ToInt32(new[] { Convert.ToInt32(proj.Variables["sessionPauseFrom"].Value),
                                    Convert.ToInt32(proj.Variables["sessionPauseTo"].Value)}.Average()); // Новая сессия через
            Variables.sessionDuaration = Convert.ToInt32(new[] { Convert.ToInt32(proj.Variables["sessionDurationFrom"].Value),
                                    Convert.ToInt32(proj.Variables["sessionDurationTo"].Value)}.Average()); // Новая сессия через
            Variables.DEBUGGING = Convert.ToBoolean(proj.Variables["DEBUGGING"].Value);
            //DB
            Variables.connectionStringOddating = proj.Variables["connectionStringOddating"].Value; // Строка подключения к базе данных oddating
            //Директории
            Variables.lisaProfilesPath = proj.Variables["lisaProfilesPath"].Value; //Аккаунты LISA OD
            Variables.ODDatingProfilesPath = proj.Variables["ODDatingProfilesPath"].Value; // Аккаунты проекта
            #endregion
            #region [Вступление в группы]
            //Общие настройки
            Variables.groupsOn = Convert.ToBoolean(proj.Variables["groupsOn"].Value); //Вкл-Выкл 
            Variables.groupsToJoinFrom = Convert.ToInt32(proj.Variables["groupsToJoinFrom"].Value); // Вступить в группы от
            Variables.groupsToJoinTo = Convert.ToInt32(proj.Variables["groupsToJoinTo"].Value); // Вступить в группы до
            #endregion
            #region [Глобальные переменные]
            Variables.localWarnAndInfoLogPath = proj.GlobalVariables["LogLevels", "localWarnAndInfoLogPath"].Value;
            Variables.localTraceAndDebugLogPath = proj.GlobalVariables["LogLevels", "localTraceAndDebugLogPath"].Value;
            Variables.localFatalAndErrorLogPath = proj.GlobalVariables["LogLevels", "localFatalAndErrorLogPath"].Value;
            Variables.generalWarnAndInfoLogPath = proj.GlobalVariables["LogLevels", "generalWarnAndInfoLogPath"].Value;
            Variables.generalTraceAndDebugLogPath = proj.GlobalVariables["LogLevels", "generalTraceAndDebugLogPath"].Value;
            Variables.generalFatalAndErrorLogPath = proj.GlobalVariables["LogLevels", "generalFatalAndErrorLogPath"].Value;
            #endregion
            #region [URLs начальных страниц действий]
            Variables.groupsToJoinTo = Convert.ToInt32(proj.Variables["groupsToJoinTo"].Value); // Вступить в группы до
            #endregion
        }

        public void ConfigurateProgram(Instance inst, IZennoPosterProjectModel proj)
        {
            Program.Npg = new Npg(Variables.connectionStringOddating, "select * from main; select * from groups", true, DBContext.DataSet);
            DBContext.Main = DBContext.DataSet.Tables["main"];
            DBContext.Groups = DBContext.DataSet.Tables["groups"];
        }
        public void ConfigurateNpg()
        {

        }
        public void ConfigurateAppCahcesCollection()
        {
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
        }
        public void ConfigurateHost()  
        {
            HostConfiguration.FillProjectProfilesDirectory();
        }
        public void ConfigurateDB()
        {
            DBConfifuration.OddatingMain.RefreshProfileColumn();
            DBConfifuration.OddatingMain.NewDayRefreshColumns();
            Program.Npg.UpdateOuter();
        }
    }
}

