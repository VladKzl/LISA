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
        public DataSet DataSet { get; set; } = new DataSet();
        public DataTable Main { get; set; }
        public DataTable Groups { get; set; }
        public NpgsqlConnection Connection { get; set; }
        public NpgsqlCommand SelectCommand { get; set; }
        public NpgsqlDataAdapter Adapter { get; set; }
        public NpgsqlCommandBuilder CommandBuilder { get; set; }
        public Npg(string connectionString, string selectString, bool autoFill, DataSet outerDataSet = null, bool creatingCommandBuilder = true)
        {
            Connection = new NpgsqlConnection() { ConnectionString = connectionString };
            SelectCommand = new NpgsqlCommand() { CommandText = selectString, Connection = Connection };
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
        public void UpdateOuter(DataSet dataSet, bool aceptChanges = true)
        {
            Adapter.Update(dataSet);
            if (aceptChanges) dataSet.AcceptChanges();
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
        }
        private void FillDataSet(DataSet outerDataSet = null)
        {
            if (outerDataSet != null)
            {
                Adapter.FillSchema(outerDataSet, SchemaType.Mapped);
                Adapter.Fill(outerDataSet);
                ConfigurateDataSet();
            }
            else
            {
                Adapter.FillSchema(DataSet, SchemaType.Mapped);
                Adapter.Fill(DataSet);
                ConfigurateDataSet();
            }
        }
        private void ConfigurateDataSet(DataSet outerDataSet = null)
        {
            if (outerDataSet != null)
            {
                // Костыль. Устанавливаем default для столбцов, так как при апдейте они не появляются автоматически.
                outerDataSet.Tables["main"].Columns["filling"].DefaultValue = "No";
                outerDataSet.Tables["main"].Columns["status"].DefaultValue = "Ready";
                outerDataSet.Tables["main"].Columns["session_ending"].DefaultValue = DateTime.Now;/*TimeSpan.Parse(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));*/
                outerDataSet.Tables["main"].Columns["sessions_count"].DefaultValue = 0;
                outerDataSet.Tables["main"].Columns["moves_count"].DefaultValue = 0;
            }
            else
            {
                DataSet.Tables["main"].Columns["filling"].DefaultValue = "No";
                DataSet.Tables["main"].Columns["status"].DefaultValue = "Ready";
                DataSet.Tables["main"].Columns["session_ending"].DefaultValue = DateTime.Now;/
                DataSet.Tables["main"].Columns["sessions_count"].DefaultValue = 0;
                DataSet.Tables["main"].Columns["moves_count"].DefaultValue = 0;
            }
        }
    }
}
