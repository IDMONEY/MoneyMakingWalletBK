#region Libraries
using System.Data;
#endregion

namespace IDMONEY.IO.Data
{
    public static class DataParameterBuilderExtensions
    {
        /// <summary>
        /// Adds a new input data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <returns>The current data parameter builder instance.</returns>
        public static IDataParameterBuilder AddInParameter(this IDataParameterBuilder builder, string name, DbType type, object value)
        {
            Ensure.IsNotNull(builder);

            builder.AddParameter(name, type, value, ParameterDirection.Input);
            return builder;
        }

        /// <summary>
        /// Adds a new output data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <returns>The current data parameter builder instance.</returns>
        public static IDataParameterBuilder AddOutParameter(this IDataParameterBuilder builder, string name, DbType type, object value)
        {
            Ensure.IsNotNull(builder);

            builder.AddParameter(name, type, value, ParameterDirection.Output);
            return builder;
        }
    }
}
