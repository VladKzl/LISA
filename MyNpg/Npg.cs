using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MyNpg
{
    public class Npg
    {
        private DataSet OuterDataSet { get; set; }
        public DataSet DataSet { get; set; } = new DataSet();
        public NpgsqlConnection Connection { get; set; }
        public NpgsqlCommand SelectCommand { get; set; }
        public NpgsqlDataAdapter Adapter { get; set; }
        public NpgsqlCommandBuilder CommandBuilder { get; set; }
        public Npg(string connectionString, string selectString, bool autoFill, DataSet outerDataSet = null, bool creatingCommandBuilder = true)
        {
            Connection = new NpgsqlConnection() { ConnectionString = connectionString };
            SelectCommand = new NpgsqlCommand() { CommandText = selectString, Connection = Connection };
            OuterDataSet = outerDataSet;
            ConfigurateDataAdapter();
            if (autoFill) {
                FillDataSet(outerDataSet);
            }
            if (creatingCommandBuilder) {
                CommandBuilder = new NpgsqlCommandBuilder(Adapter);
            }
        }
        public void Open()
        {
            Connection.Open();
        }
        public void Close()
        {
            if (Connection?.State != ConnectionState.Closed)
            {
                Connection?.Close();
            }
        }
        public void UpdateOuter(bool aceptChanges = true)
        {
            Adapter.Update(OuterDataSet);
            if (aceptChanges) OuterDataSet.AcceptChanges();
        }
        public void UpdateInner(bool aceptChanges = true)
        {
            Adapter.Update(DataSet);
            if (aceptChanges) DataSet.AcceptChanges();
        }
        private void ConfigurateDataAdapter()
        {
            Adapter = new NpgsqlDataAdapter(SelectCommand);
            Adapter.MissingMappingAction = MissingMappingAction.Passthrough;
            Adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            Adapter.TableMappings.Add("Table", "main");
            Adapter.TableMappings.Add("Table1", "groups");
            Adapter.TableMappings.Add("Table2", "groups_statistics");
        }
        private void FillDataSet(DataSet outerDataSet = null)
        {
            if (outerDataSet != null)
            {
                Adapter.FillSchema(OuterDataSet, SchemaType.Mapped);
                Adapter.Fill(OuterDataSet);
                ConfigurateDataSet();
            }
            else
            {
                Adapter.FillSchema(DataSet, SchemaType.Mapped);
                Adapter.Fill(DataSet);
                ConfigurateDataSet();
            }
        }
        private void ConfigurateDataSet()
        {
            if (OuterDataSet != null)
            {
                // Костыль. Устанавливаем default для столбцов, так как при апдейте они не появляются автоматически.
                OuterDataSet.Tables["main"].Columns["filling"].DefaultValue = "No";
                OuterDataSet.Tables["main"].Columns["status"].DefaultValue = "Ready";
                OuterDataSet.Tables["main"].Columns["session_ending"].DefaultValue = DateTime.Now;/*TimeSpan.Parse(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));*/
                OuterDataSet.Tables["main"].Columns["sessions_count"].DefaultValue = 0;
                OuterDataSet.Tables["main"].Columns["moves_count"].DefaultValue = 0;
            }
            else
            {
                DataSet.Tables["main"].Columns["filling"].DefaultValue = "No";
                DataSet.Tables["main"].Columns["status"].DefaultValue = "Ready";
                DataSet.Tables["main"].Columns["session_ending"].DefaultValue = DateTime.Now;
                DataSet.Tables["main"].Columns["sessions_count"].DefaultValue = 0;
                DataSet.Tables["main"].Columns["moves_count"].DefaultValue = 0;
            }
        }
    }
}
