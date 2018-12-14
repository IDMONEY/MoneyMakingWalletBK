#region Libraries
using System;

#endregion
namespace IDMONEY.IO
{
    internal static class ConvertUtil
    {
        public static T ValueOrDefault<T>(object value, T defaultValue)
        {
            if (value is DBNull || value.IsNull())
            {
                return defaultValue;
            }

            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            return (T)Convert.ChangeType(value, conversionType);
        }
    }
}