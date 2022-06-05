using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODDating.Interfaces
{
    interface ILISA : ILSAZPSettings, ILISAMainCommon
    {
        string Account { get; set; }
        DataRow AccountRow { get; set; }
        int AccountRowNumber { get; set; }
        Type[] RegisteredModules { get; set; }
        int StartTimeTotalMinutes { get; }
    }
}
