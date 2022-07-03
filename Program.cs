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
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Configuration;
using System.Globalization;
using ODDating.Configs;
using static ODDating.Variables;
using MyNpg;
using LogLevels;
using ODDating.Interfaces;
using ZPBase;
using LISA;
using ODDating.Entityes;
using static ZPExtensionsMethods.HtmlEllementExtentions;
using static ZPExtensionsMethods.MoveCheckExtensions;
using static ZPExtensionsMethods.MoveCheckExtensions;
using ODDating.DBHelpers;

namespace ODDating
{
    public class Program : IZennoExternalCode
    {
        public static Npg Npg { get; set; }
        public int Execute(Instance instance, IZennoPosterProjectModel project) // main
        {
            try
            {
                StartAppConfiguration(instance, project);

                HelperGroups.AddToUsedGroups();

                ActionsControl LISA = new ActionsControl();
                LISA.StartActions();
            }
            finally
            {

            }
            return 0;
        }
        private void StartAppConfiguration(Instance instance, IZennoPosterProjectModel project)
        {
            lock (lockerAppConfiguration)
            {
                if (configurateAppOneTime)
                {
                    new AppConfiguration(instance, project);
                    configurateAppOneTime = false;
                }
            }
            DBConfifuration.OddatingMain.UpdateIfNewDayBegins();
        }
    }
}
