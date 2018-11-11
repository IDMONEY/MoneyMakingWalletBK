#region Libraries
using System;
using System.Diagnostics;
using System.Linq;
#endregion

namespace IDMONEY.IO
{
    /// <summary>
    /// Static class which contains extension methods of <see cref="Enum"/>.
    /// </summary>

    public static class EnumExtensions
    {

        /// <summary>
        /// Convert the enum  to another one using the name.
        /// </summary>
        /// <param name="source">The enum source.</param>
        /// <returns>The enum representation of the enum by name.</returns>
        [DebuggerStepThrough]

        public static TEnumDestination ConvertByName<TEnumDestination>(this Enum source)
           where TEnumDestination : struct, IConvertible
        {
            Ensure.IsNotNull(source);

            return (TEnumDestination)Enum.Parse(typeof(TEnumDestination), source.ToString());
        }

        /// <summary>
        /// Gets the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum; otherwise, it will return
        /// the default enum value string representation.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>
        /// The string value assigned with the StringValue attribute;
        /// otherwise, the default enum value string representation.
        /// </returns>
        /// 
        [DebuggerStepThrough]
        public static string GetStringValue(this Enum value)
        {
            StringValueAttribute attribute = value.GetType()
                                                  .GetField(value.ToString())
                                                  .GetCustomAttributes<StringValueAttribute>(false)
                                                  .FirstOrDefault();
            return (attribute.IsNotNull()) ? attribute.StringValue : value.ToString();
        }
    }
}
