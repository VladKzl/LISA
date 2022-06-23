using LISA.Interfaces;
using ODDating.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ODDating.Variables;
using static ZPBase.Base;

namespace LISA
{
    enum AccountStatus
    {
        Ready,
        Done,
        Work,
        Off
    }
    public class Common : ICommon
    {
        public string Account { get; set; }
        public DataRow AccountRow { get; set; }
        public int AccountRowNumber { get; set; }
        public Type[] RegisteredActions { get; set; }
        public int StartTimeTotalMinutes
        {
            get => (int)(DateTime.Now - (DateTime)AccountRow["session_ending"]).TotalMinutes;
        }
        public int AmountMoves { get; set; } = amountMoves;
        public int MovePause { get; set; } = movePause;
        public int SessionPause { get; set; } = sessionPause;
        public int SessionDuration { get; set; } = sessionDuaration;
    }
}
