using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZPBase
{
    interface IZPBase
    {
        Instance Instance { get; set; }
        IZennoPosterProjectModel Project { get; set; }
    }
}
