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
        public Npg(string connectionString,
                     string selectString,
                     bool autoFill,
                     bool creatingCommandBuilder = true)
        {
            Connection = new NpgsqlConnection() {ConnectionString = connectionString };
            Command = new NpgsqlCommand() { CommandText = selectString, Connection = Connection };
            Adapter = new NpgsqlDataAdapter(Command)
            {
                MissingMappingAction = MissingMappingAction.Passthrough,
                MissingSchemaAction = MissingSchemaAction.AddWithKey,
            };
            Adapter.TableMappings.Add("Table", "main");
            Adapter.TableMappings.Add("Table1", "groups");
            if (autoFill)
            {
                Adapter.Fill(DataSet);
            }
            if (creatingCommandBuilder)
            {
                CommandBuilder = new NpgsqlCommandBuilder(Adapter);
            }
        }
        public Npg(string connectionString,
                     string selectString,
                     DataSet outerDataSet,
                     bool autoFill,
                     bool creatingCommandBuilder = true) : this(connectionString, selectString, creatingCommandBuilder)
        {
            if (autoFill)
            {
                Adapter.Fill(outerDataSet);
            }
        }
        public DataSet DataSet { get; set; } = new DataSet();
        public DataTable Main { get; set; } 
        public DataTable Groups { get; set; }
        public NpgsqlConnection Connection { get; set; }
        public NpgsqlCommand Command { get; set; }
        public NpgsqlDataAdapter Adapter { get; set; }
        public NpgsqlCommandBuilder CommandBuilder { get; set; }
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
    }
}
