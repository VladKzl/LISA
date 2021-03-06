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
using static ODDating.Variables;
using static ZPBase.Base;

namespace LogLevels
{
    public class Error : Exception
    {
        public Error(string message, bool logMassage = true, bool zennoLogMassage = true, bool nlogMassage = false) : base(message)
        {
            string currentThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            string sample = $"{currentThreadId}|{DateTime.Now.ToLongTimeString()}|{typeof(Error).Name.ToUpper()}| {message}" + Environment.NewLine;
            if (logMassage == true)
            {
                lock (lockerLogMassage)
                {
                    string generalPath = Path.Combine(generalFatalAndErrorLogPath, DateTime.Now.ToShortDateString() + ".txt");
                    string localPath = Path.Combine(localFatalAndErrorLogPath, DateTime.Now.ToShortDateString() + ".txt");

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
            if (zennoLogMassage == true)
            {
                lock (lockerZennoLogMassage)
                {
                    project.SendErrorToLog(sample, true);
                }
            }
            if (nlogMassage == true)
            {
                lock (lockerNlogMassage)
                {
                    Logger Logger = LogManager.GetLogger("Error");
                    Logger.Error(message);
                }
            }
        }
    }
}