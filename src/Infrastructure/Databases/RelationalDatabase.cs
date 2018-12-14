#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
#endregion

namespace IDMONEY.IO.Databases
{
    public abstract class RelationalDatabase : IDisposable
    {
        public MySqlConnection Connection { get; set; }

        public RelationalDatabase()
        {
            Connection = new MySqlConnection()
            {
                ConnectionString = DataBaseContext.CONNECTION_STRING
            };
            Connection.Open();

        }

        public IDataReader ExecuteQuery(string stringQuery)
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
        public virtual void ExecuteReader(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, Action<IDataReader> action)
        {
            this.Execute<bool>(commandText, commandType, parameters,
                                (command) =>
                                {
                                    IDataReader reader = command.ExecuteReader();

                                    action(reader);
                                    if (!reader.IsClosed)
                                    {
                                        reader.Close();
                                    }
                                    return true;
                                });
        }

        protected virtual TResult Execute<TResult>(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, Func<IDbCommand, TResult> executor)
        {
            var command = this.CreateCommand(commandText, commandType, parameters, this.Connection);
            return executor(command);
        }

        protected virtual IDbCommand CreateCommand(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, DbConnection connection)
        {
            return connection.CreateCommand(commandText, commandType, parameters);
        }



        public void Dispose()
        {
            Connection.Close();
        }

        protected virtual void MapEntity<TEntity>(IDataReader reader, ref TEntity entity, Func<IDataReader, TEntity> mapper)
        {
            if (reader.Read())
            {
                entity = mapper(reader);
            }
        }

        protected virtual void MapEntities<TEntity>(IDataReader reader, ref IList<TEntity> entities, Func<IDataReader, TEntity> mapper)
        {
            while (reader.Read())
            {
                TEntity entity = mapper(reader);
                entities.Add(entity);
            }
        }

        protected virtual DbProviderFactory GetFactory() =>
              MySqlClientFactory.Instance;

    }
}
