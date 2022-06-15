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
    public class Base
    {
        [ThreadStatic]
        public static Instance instance;
        [ThreadStatic]
        public static IZennoPosterProjectModel project;
/*        private Instance Instance
        {
            get => instance;
            set { instance = value; }
        }
        public IZennoPosterProjectModel Project
        {
            get => project;
            set { project = value; }
        }*/
    }
}
