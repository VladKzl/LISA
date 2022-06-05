using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODDating.Interfaces
{
    interface ILISAMainCommon
    {
        DateTime Session_ending { get; set; }
        int Sessions_count { get; set; }
        int Moves_count { get; set; }
        string Status { get; set; }

    }
}
