using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ODDating.Interfaces
{
    interface IAppConfiguration
    {
        void ConfigurateVariables(Instance instance, IZennoPosterProjectModel proj);
        void ConfigurateProgram(Instance instance, IZennoPosterProjectModel proj);
        void ConfigurateAppCahcesCollection();
        void ConfigurateHost();
        void ConfigurateDB();
    }
}
