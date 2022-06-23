using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using MyNpg;

namespace ZPBase
{
    public static class Base
    {
        [ThreadStatic]
        public static Instance instance;
        [ThreadStatic]
        public static IZennoPosterProjectModel project;
    }
}
