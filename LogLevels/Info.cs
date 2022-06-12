using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.Emulation;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;
using NLog;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using static ODDating.Program;
using static ODDating.Variables;

namespace LogLevels
{
    public class Info
    {
        public Info(string message, bool logMassage = true, bool zennoLogMassage = true, bool nlogMassage = false)
        {
                string currentThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
                string sample = $"{currentThreadId}|{DateTime.Now.ToLongTimeString()}|{typeof(Info).Name.ToUpper()}| {message}" + Environment.NewLine;
                if (logMassage == true)
                {
                    lock (lockerLogMassage)
                    {
                        string generalPath = Path.Combine(generalWarnAndInfoLogPath, DateTime.Now.ToShortDateString() + ".txt");
                        string localPath = Path.Combine(localWarnAndInfoLogPath, DateTime.Now.ToShortDateString() + ".txt");

                        File.AppendAllText(generalPath, sample);
                        File.AppendAllText(localPath, sample);

                        string[] listArray = File.ReadLines(generalPath).ToArray();
                        Array.Sort(listArray);
                        var listList = listArray.ToList();
                        listList.RemoveAll(x => x == string.Empty);

                        File.Delete(generalPath);
                        File.Delete(localPath);
                        File.AppendAllLines(generalPath, listList);
                        File.AppendAllLines(localPath, listList);
                    }
                }
                if (zennoLogMassage)
                {
                    lock (lockerZennoLogMassage)
                    {
                        project.SendInfoToLog(sample, true);
                    }
                }
                if (nlogMassage)
                {
                    lock (lockerNlogMassage)
                    {
                        Logger Logger = LogManager.GetLogger("Info");
                        Logger.Info(message);
                    }
                }
        }
    }
}
