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
using ODDating.MyNpg;
using ODDating.LogLevels;
using ODDating.ModulesControl;

namespace ODDating
{
    public class Program : IZennoExternalCode
    {
        [ThreadStatic]
        public static Instance instance;
        [ThreadStatic]
        public static IZennoPosterProjectModel project;
        public static Npg NpgObjects { get; set; }
        public static DataTable Main { get; set; }
        public static DataTable Groups { get; set; }
        public int Execute(Instance instance, IZennoPosterProjectModel project) // main
        {
            try
            {
                StartAppConfiguration(instance, project);
                new LISA().StartModules();
            }
            catch (Error ex)
            {
                string error = ex.Message;
            }
            catch (Fatal ex)
            {
                string fatal = ex.Message;
            }
/*            catch (Exception ex)
            {
                new Fatal("Exeption" + ex.Message);
            }*/
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
        }
    }
}
