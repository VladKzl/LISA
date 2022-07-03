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
using ODDating.DBHelpers;

namespace ODDating
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(Instance instance, IZennoPosterProjectModel project)
        {
            ZPBaseConfiguration(instance, project);
            ConfigurateVariables(instance, project);
            ConfigurateProgram(instance, project);
            ConfigurateAppCahcesCollection();
            ConfigurateHost();
            ConfigurateDB();
            ConfigurateDBHelpers();
        }
        public void ZPBaseConfiguration(Instance _instance, IZennoPosterProjectModel _project)
        {
            project = _project;
            instance = _instance;
        }
        public void ConfigurateVariables(Instance instance, IZennoPosterProjectModel project)
        {
            new Variables(instance, project);
        }
        public void ConfigurateAppCahcesCollection()
        {
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
            AppCachesCollection.ConfigurationCache = new MemoryCache(new MemoryCacheOptions());
        }
        public void ConfigurateProgram(Instance instance, IZennoPosterProjectModel project)
        {
            Program.Npg = new Npg(Variables.connectionStringOddating,
                "select * from main;" +
                "select * from groups;" +
                "select * from groups_statistics;", true, DBContext.DataSet);
            DBContext.Main = DBContext.DataSet.Tables["main"];
            DBContext.Groups = DBContext.DataSet.Tables["groups"];
            DBContext.GroupsStatistics = DBContext.DataSet.Tables["groups_statistics"];
        }
        public void ConfigurateHost()
        {
            HostConfiguration.FillProjectProfilesDirectory();
        }
        public void ConfigurateDB()
        {
            DBConfifuration.OddatingMain.RefreshProfileColumn();
            Program.Npg.UpdateOuter();
        }
        public void ConfigurateDBHelpers()
        {
            new HelperGroups();
        }
    }
}

