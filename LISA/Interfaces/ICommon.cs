﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISA.Interfaces
{
    interface ICommon : IZPSettings
    {
        string Account { get; set; }
        DataRow AccountRow { get; set; }
        int AccountRowNumber { get; set; }
        Type[] RegisteredActions { get; set; }
        int StartTimeTotalMinutes { get; }
    }
}