using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace IDMONEY.IO.DataAccess
{
    public abstract class DataAccess : IDisposable
    {

        public MySqlConnection Connection { get; set; }

        public DataAccess()
        {
            Connection = new MySqlConnection()
            {
                ConnectionString = DataBaseContext.CONNECTION_STRING
            };
            //Connection.Open();
        }

        public MySqlDataReader ExecuteQuery(string stringQuery)
        {
            MySqlDataReader reader = null;
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = stringQuery;
                reader = command.ExecuteReader();
            }

            return reader;
        }

        public void ExecuteNonQuery(string stringQuery)
        {
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = stringQuery;
                command.ExecuteNonQuery();
            }
        }

        public long ExecuteQuery(string stringQuery, List<ParameterSchema> lstParameters)
        {
            long idInsert = 0;
            using (MySqlCommand command = Connection.CreateCommand())
            {
                for (int i = 0; i < lstParameters.Count; i++)
                {
                    command.Parameters.AddWithValue("@" + lstParameters[i].ParamName, lstParameters[i].ParamValue);
                }

                command.CommandText = stringQuery;

                if (command.ExecuteNonQuery() > 0)
                {
                    idInsert = command.LastInsertedId;
                }
            }

            return idInsert;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }

    public class ParameterSchema
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }

        public ParameterSchema() { }

        public ParameterSchema(string ParamName, object ParamValue)
        {
            this.ParamName = ParamName;
            this.ParamValue = ParamValue;
        }
    }
}
