using ODDating.ActionsControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZPBase;
using static ODDating.Variables;

namespace ODDating.Actions
{
    public abstract class MoveBase
    {
        public int MovePause { get; set; } = movePause;
        public Instance instance { get; set; } = ProgramBase.instance;
        public IZennoPosterProjectModel project { get; set; } = ProgramBase.project;
        public string Account { get; set; } = new LISA().Account;
        public DataRow AccountRow { get; set; } = new LISA().AccountRow;
        public int AccountRowNumber { get; set; } = new LISA().AccountRowNumber;
    }
}
