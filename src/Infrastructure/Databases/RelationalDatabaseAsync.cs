#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
#endregion

namespace IDMONEY.IO.Databases
{
    public abstract class RelationalDatabaseAsync : IDisposable
    {
        public MySqlConnection Connection { get; set; }

        public RelationalDatabaseAsync()
        {
            Connection = new MySqlConnection()
            {
                ConnectionString = DataBaseContext.CONNECTION_STRING
            };
            Connection.OpenAsync();

        }

        public async Task<IDataReader> ExecuteQueryAsync(string stringQuery)
        {
            IDataReader reader = default(IDataReader);
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = stringQuery;
                reader = await command.ExecuteReaderAsync();
            }

            return reader;
        }

        public async Task<int> ExecuteNonQueryAsync(string stringQuery)
        {
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = stringQuery;
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<long> ExecuteQueryAsync(string stringQuery, List<ParameterSchema> lstParameters)
        {
            long idInsert = 0;
            using (MySqlCommand command = Connection.CreateCommand())
            {
                for (int i = 0; i < lstParameters.Count; i++)
                {
                    command.Parameters.AddWithValue("@" + lstParameters[i].ParamName, lstParameters[i].ParamValue);
                }

                command.CommandText = stringQuery;

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    idInsert = command.LastInsertedId;
                }
            }

            return idInsert;
        }
        public virtual async Task<bool> ExecuteReaderAsync(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, Action<IDataReader> action)
        {
            await this.ExecuteAsync<bool>(commandText, commandType, parameters,
                                async (command) =>
                                {
                                    IDataReader reader = await command.ExecuteReaderAsync();

                                    action(reader);
                                    if (!reader.IsClosed)
                                    {
                                        reader.Close();
                                    }
                                    return await Task.FromResult(true);
                                });

            return await Task.FromResult(true);
        }

        protected virtual async Task<TResult> ExecuteAsync<TResult>(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, Func<DbCommand, Task<TResult>> executor)
        {
            var command = this.CreateCommand(commandText, commandType, parameters, this.Connection);
            return await executor(command);
        }

        protected virtual DbCommand CreateCommand(string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters, DbConnection connection)
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
