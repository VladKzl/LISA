using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ODDating.MyNpg
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
            ConfigurateDataAdapter(outerDataSet);
            if (autoFill) {
                FillDataSet(autoFill, outerDataSet);
            }
            ConfigurateDataSet();
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
        private void ConfigurateDataAdapter(DataSet outerDataSet = null)
        {
            Adapter = new NpgsqlDataAdapter(SelectCommand);
            Adapter.MissingMappingAction = MissingMappingAction.Passthrough;
            Adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            Adapter.TableMappings.Add("Table", "main");
            Adapter.TableMappings.Add("Table1", "groups");
        }
        private void FillDataSet(bool autoFill, DataSet outerDataSet = null)
        {
            if (outerDataSet != null)
            {
                Adapter.FillSchema(outerDataSet, SchemaType.Mapped);
                Adapter.Fill(outerDataSet);
            }
            else
            {
                Adapter.FillSchema(DataSet, SchemaType.Mapped);
                Adapter.Fill(DataSet);
            }
        }
        private void ConfigurateDataSet()
        {
            // Костыль. Устанавливаем default для столбцов, так как при апдейте они не появляются автоматически.
            DataSet.Tables["main"].Columns["filling"].DefaultValue = "No";
            DataSet.Tables["main"].Columns["session_ending"].DefaultValue = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
            DataSet.Tables["main"].Columns["status"].DefaultValue = "Ready";
        }
    }
}
