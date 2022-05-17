using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using ZennoLab.CommandCenter;
using ZennoLab.Emulation;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using ODDating.Configs;
using Microsoft.Extensions.Internal;

namespace ODDating
{
    public static class AppCachesCollection
    {
        //MemoryCacheOptions
        //MemoryCacheEntryOptions
        public static MemoryCache ConfigurationCache { get; set; }
        public static MemoryCache GeneralCache { get; set; }
    }
}
