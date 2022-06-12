using ODDating.Interfaces;
using LogLevels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ODDating.ActionsControl;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ODDating;
using System.Data;
using ZPBase;

namespace ODDating.Actions
{
    public abstract class ActionBase : IAction
    {
        public Instance instance { get; set; } = ProgramBase.instance;
        public IZennoPosterProjectModel project { get; set; } = ProgramBase.project;
        public string Account { get; set; } = new LISA().Account;
        public DataRow AccountRow { get; set; } = new LISA().AccountRow;
        public int AccountRowNumber { get; set; } = new LISA().AccountRowNumber;
        public abstract string StartPageUrl { get; set; }
        public ActionBase()
        {
        }
        public abstract void Move0_BrowseStartPage();
        public abstract void RunAction();
    }
}
