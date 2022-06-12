using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ODDating.MyNpg;

namespace ZPBase
{
    public abstract class ProgramBase
    {
        [ThreadStatic]
        public static Instance instance;
        [ThreadStatic]
        public static IZennoPosterProjectModel project;
    }
}
