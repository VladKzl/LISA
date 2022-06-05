using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODDating.Interfaces
{
    interface ILSAZPSettings
    {
        int AmountMoves { get; set; }
        int MovePause { get; set; }
        int SessionPause { get; set; }
        int SessionDuration { get; set; }
    }
}
