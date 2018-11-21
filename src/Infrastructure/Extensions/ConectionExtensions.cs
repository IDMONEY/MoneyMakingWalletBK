#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq; 
#endregion

namespace IDMONEY.IO
{
    internal static class ConectionExtensions
    {
        #region Methods

        public static IDbCommand CreateCommand(this IDbConnection connection, string commandText, CommandType commandType, IEnumerable<IDataParameter> parameters)
        {
            IDbCommand command = connection.CreateCommand();
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
