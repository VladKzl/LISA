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
using ODDating.MyNpg;
using ODDating.Configs;
using Microsoft.Extensions.Caching.Memory;

namespace ODDating
{
    public class AppConfiguration
    {
        public AppConfiguration(Instance inst, IZennoPosterProjectModel proj)
        {
            ConfigurateVariables(inst, proj);
            ConfigurateProgram(inst, proj);
            ConfigurateAppCahcesCollection();
            ConfigurateHost();
            ConfigurateDB();
        }
        private void ConfigurateVariables(Instance inst, IZennoPosterProjectModel proj)
        {
            #region [Main]
            //Общие настройки
            Variables.amountMovesTo = Convert.ToInt32(proj.Variables["amountMovesTo"].Value); // Всего действий до
            Variables.amountMovesFrom = Convert.ToInt32(proj.Variables["amountMovesFrom"].Value); // Всего действий от
            Variables.movePauseFrom = Convert.ToInt32(proj.Variables["movePauseFrom"].Value); // Пауза между действиями от
            Variables.movePauseTo = Convert.ToInt32(proj.Variables["movePauseTo"].Value); // Пауза между действиями до
            Variables.sessionPauseFrom = Convert.ToInt32(proj.Variables["sessionPauseFrom"].Value); // Новая сессия через от
            Variables.sessionPauseTo = Convert.ToInt32(proj.Variables["sessionPauseTo"].Value); // Новая сессия через от
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
            Variables.localWarnAndInfoLogPath = proj.GlobalVariables["LogLevels","localWarnAndInfoLogPath"].Value;
            Variables.localTraceAndDebugLogPath = proj.GlobalVariables["LogLevels","localTraceAndDebugLogPath"].Value;
            Variables.localFatalAndErrorLogPath = proj.GlobalVariables["LogLevels","localFatalAndErrorLogPath"].Value;
            Variables.generalWarnAndInfoLogPath = proj.GlobalVariables["LogLevels","generalWarnAndInfoLogPath"].Value; 
            Variables.generalTraceAndDebugLogPath = proj.GlobalVariables["LogLevels","generalTraceAndDebugLogPath"].Value;
            Variables.generalFatalAndErrorLogPath = proj.GlobalVariables["LogLevels", "generalFatalAndErrorLogPath"].Value;
            #endregion
        }
        private void ConfigurateProgram(Instance inst, IZennoPosterProjectModel proj)
        {
            Program.instance = inst;
            Program.project = proj;
            Program.NpgObjects = new Npg(Variables.connectionStringOddating, "select * from main; select * from groups", true);
            Program.Main = Program.NpgObjects.DataSet.Tables["main"];
            Program.Groups = Program.NpgObjects.DataSet.Tables["groups"];
        }
        private void ConfigurateAppCahcesCollection()
        {
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
        }
        private void ConfigurateHost()  
        {
            HostConfiguration.FillProjectProfilesDirectory();
        }
        private void ConfigurateDB()
        {
            DBConfifuration.OddatingMain.RefreshProfileColumn();
        }
    }
}

