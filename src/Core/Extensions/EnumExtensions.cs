using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
    }
}
