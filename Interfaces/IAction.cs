using ODDating.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ODDating.Interfaces
{
    interface IAction
    {
        Instance instance { get; set; }
        IZennoPosterProjectModel project { get; set; }
        string Account { get; set; }
        string StartPageUrl { get; set; }
        void Move0_BrowseStartPage(int tabNum);
        void RunAction();
    }
}
