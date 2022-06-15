using ODDating.ActionsControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using static ODDating.Variables;
using ZPBase;

namespace ODDating.ProjectBase
{
    public abstract class MoveBase
    {
        public int MovePause { get; set; } = movePause;
        public Instance instance { get; set; } = Base.instance;
        public IZennoPosterProjectModel project { get; set; } = Base.project;
        public string Account { get; set; } = new LISA().Account;
        public DataRow AccountRow { get; set; } = new LISA().AccountRow;
        public int AccountRowNumber { get; set; } = new LISA().AccountRowNumber;
    }
}
