using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISA.Interfaces
{
    interface ICommon : IZPSettings
    {
        string AccountPath { get; set; }
        string AccountName { get; set; }
        DataRow AccountRow { get; set; }
        int AccountRowNumber { get; set; }
        List<Type> RegisteredActions { get; set; }
        int StartTimeTotalMinutes { get; }
    }
}
