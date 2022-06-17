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
        bool ON { get; set; }
        string Account { get; set; }
        string StartPageUrl { get; set; }
        void RunAction();
    }
}
