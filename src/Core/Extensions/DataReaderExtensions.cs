using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IDMONEY.IO
{
    public static class DataReaderExtension
    {
        /// <summary>
        /// Provides strongly-typed access to each of the column values in the specified
        /// data reader. If the value is null then returns the default value;
        /// </summary>
        /// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
        /// <param name="reader">The input <see cref="IDataReader"/>, which acts as the this instance for the extension method.</param>
        /// <param name="columnName">The name of the column to return the value of.</param>
        /// <returns>The value, of type T, of the IDataReader value specified by columnName.</returns>
        public static T FieldOrDefault<T>(this IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return ConvertUtil.ValueOrDefault(value, default(T));
        }

        /// <summary>
        /// Provides strongly-typed access to each of the column values in the specified
        /// data reader. If the value is null then returns the provided default value;
        /// </summary>
        /// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
        /// <param name="reader">The input <see cref="IDataReader"/>, which acts as the this instance for the extension method.</param>
        /// <param name="columnName">The name of the column to return the value of.</param>
        /// <param name="defaultValue">The default value to return if the original value is null.</param>
        /// <returns>The value, of type T, of the IDataReader value specified by columnName.</returns>
        public static T FieldOrDefault<T>(this IDataReader reader, string columnName, T defaultValue)
        {
            object value = reader[columnName];
            return ConvertUtil.ValueOrDefault(value, defaultValue);
        }
    }

}
