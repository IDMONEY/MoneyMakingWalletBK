#region Libraries
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq; 
#endregion

namespace IDMONEY.IO.Data
{
    public class DataParameterBuilder : IDataParameterBuilder
    {
        private readonly IList<IDataParameter> parameters;
        private readonly DbProviderFactory factory;

        private DataParameterBuilder(DbProviderFactory factory)
        {
            Ensure.IsNotNull(factory);

            this.factory = factory;
            this.parameters = new List<IDataParameter>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="IDataParameterBuilder"/>.
        /// </summary>
        /// <param name="factory">The database provider factory.</param>
        /// <returns>A new instance of <see cref="IDataParameterBuilder"/>.</returns>
        public static IDataParameterBuilder Create(DbProviderFactory factory)
        {
            return new DataParameterBuilder(factory);
        }

        /// <summary>
        /// Gets the current parameters.
        /// </summary>
        public IEnumerable<IDataParameter> Parameters { get { return this.parameters.AsEnumerable(); } }

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="parameter">The data parameter.</param>
        /// <returns>The current data parameter builder instance.</returns>
        public IDataParameterBuilder AddParameter(IDataParameter parameter)
        {
            Ensure.IsNotNull(parameter);

            this.parameters.Add(parameter);
            return this;
        }

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <param name="direction">The parameter's direction</param>
        /// <returns>The current data parameter builder instance.</returns>
        public IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction)
        {
            Ensure.IsNotNullOrEmpty(name);

            IDataParameter parameter = this.factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = type;
            parameter.Direction = direction;

            this.parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// Adds a new data parameter.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="type">The parameter's DbType.</param>
        /// <param name="value">The parameter's value.</param>
        /// <param name="direction">The parameter's direction</param>
        /// <param name="size">The size of the parameter</param>
        /// <returns>The current data parameter builder instance.</returns>
        public IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction, int size)
        {
            Ensure.IsNotNullOrEmpty(name);
            Ensure.IsNotNegative(size);

            IDbDataParameter parameter = this.factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = type;
            parameter.Direction = direction;
            parameter.Size = size;
            this.parameters.Add(parameter);

            return this;
        }

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
        public IDataParameterBuilder AddParameter(string name, DbType type, object value, ParameterDirection direction, int size, byte precision, byte scale)
        {
            Ensure.IsNotNullOrEmpty(name);
            Ensure.IsNotNegative(size);
            Ensure.IsNotNegative(precision);
            Ensure.IsNotNegative(scale);

            IDbDataParameter parameter = this.factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = type;
            parameter.Direction = direction;
            parameter.Size = size;
            parameter.Precision = precision;
            parameter.Scale = scale;

            this.parameters.Add(parameter);

            return this;
        }
    }
}