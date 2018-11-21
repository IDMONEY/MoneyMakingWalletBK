#region Libraries
using System.Collections.Generic;
using System.Data; 
#endregion

namespace IDMONEY.IO.Data
{
    public interface IDataParameterBuilder : IHideObjectMembers
    {
        /// <summary>
        /// Gets the current parameters.
        /// </summary>
        IEnumerable<IDataParameter> Parameters { get; }

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="parameter">The data parameter.</param>
        /// <returns>The current data parameter builder instance.</returns>
        IDataParameterBuilder AddParameter(IDataParameter parameter);

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <param name="direction">The parameter's direction</param>
        /// <returns>The current data parameter builder instance.</returns>
        IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction);

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <param name="direction">The parameter's direction</param>
        /// <param name="size">The size of the parameter</param>
        /// <returns>The current data parameter builder instance.</returns>
        IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction, int size);

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <param name="direction">The parameter's direction</param>
        /// <param name="size">The size of the parameter</param>
        /// <param name="precision">Indicates the precision of numeric parameters.</param>
        /// <param name="scale">Indicates the scale of numeric parameters.</param>
        /// <returns>The current data parameter builder instance.</returns>
        IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction, int size, byte precision, byte scale);
    }
}