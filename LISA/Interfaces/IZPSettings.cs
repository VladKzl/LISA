using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISA.Interfaces
{
    interface IZPSettings
    {
        int AmountMoves { get; set; }
        int MovePause { get; set; }
        int SessionPause { get; set; }
        int SessionDuration { get; set; }
    }
}
