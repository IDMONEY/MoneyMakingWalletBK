#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using MySql.Data.MySqlClient;
#endregion

namespace IDMONEY.IO
{
    internal static class ConectionExtensions
    {
        #region Methods

        public static DbCommand CreateCommand(this DbConnection connection, string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters)
        {
            DbCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;
            if (parameters.Any())
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }

        #endregion Methods
    }
}
