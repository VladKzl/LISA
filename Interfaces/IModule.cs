﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODDating.Interfaces
{
    interface IModule
    {
        int MovePause { get; set; }
        void RunModule();
    }
}
