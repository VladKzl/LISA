using ODDating.Interfaces;
using LogLevels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ODDating;
using System.Data;
using LISA;
using static ODDating.Variables;

namespace ODDating.ProjectBase
{
    public abstract class ActionBase<MovesInstance> : Common, IAction
    {
        public bool ON { get; set; }
        public MovesInstance Moves { get; set; }
        public abstract void RunAction();
    }
}
