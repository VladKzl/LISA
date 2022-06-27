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
    public abstract class Common // есть интерфейс, но не используется, так как C# 7.3 не подходит
    {
        [ThreadStatic]
        public static string AccountPath;
        [ThreadStatic]
        public static string AccountName;
        [ThreadStatic]
        public static DataRow AccountRow;
        [ThreadStatic]
        public static int AccountRowNumber;
        public static List<Type> RegisteredActions { get; set; }

        public static readonly Lazy<int> StartTimeTotalMinutes = new Lazy<int>(() => 
        (int)(DateTime.Now - (DateTime)AccountRow["session_ending"]).TotalMinutes, true);
        public static int AmountMoves { get; set; } = amountMoves;
        public static int MovePause { get; set; } = movePause;
        public static int SessionPause { get; set; } = sessionPause;
        public static int SessionDuration { get; set; } = sessionDuaration;
    }
}
