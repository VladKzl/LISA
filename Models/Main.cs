using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ODDating.Variables;
using ODDating.ActionsControl;
using System.Data;
using static ODDating.Program;

namespace ODDating.Models
{
    public abstract class MainTable
    {
        private DataRow AccountRow = new LISA().AccountRow;
        private DataSet DataSet = MyNpg.StaticNpg.DataSet;
        public string Profile
        {
            get => (string)AccountRow["profile"];
            set { SetClass("profile", value); }
        }
        public int Groups_day
        {
            get => (int)AccountRow["groups_day"];
            set { SetStruct("groups_day", value); }
        }
        public int Groups_summ
        {
            get => (int)AccountRow["groups_summ"];
            set { SetStruct("groups_summ", value); }
        }
        public int Messages_day_in
        {
            get => (int)AccountRow["messages_day_in"];
            set { SetStruct("messages_day_in", value); }
        }
        public int Messages_out_summ
        {
            get => (int)AccountRow["messages_out_summ"];
            set { SetStruct("messages_out_summ", value); }
        }
        public DateTime Session_ending
        {
            get => (DateTime)AccountRow["session_ending"];
            set { SetStruct("session_ending", value); }
        }
        public int Sessions_count
        {
            get => (int)AccountRow["sessions_count"];
            set { SetStruct("sessions_count", value); }
        }
        public int Moves_count
        {
            get => (int)AccountRow["moves_count"];
            set { SetStruct("moves_count", value); }
        }
        public string Filling
        {
            get => (string)AccountRow["filling"];
            set { SetClass("filling", value); }
        }
        public string Status
        {
            get => (string)AccountRow["status"];
            set { SetClass("status", value); }
        }
        private void SetClass<T>(string columnName, T value) where T : class
        {
            lock (lockerDb) {
                AccountRow[$"{columnName}"] = value;
                Npg.UpdateInner();
            }
        }
        private void SetStruct<T>(string columnName, T value) where T : struct
        {
            lock (lockerDb) {
                AccountRow[$"{columnName}"] = value;
                Npg.UpdateInner();
            }
        }
    }
}
