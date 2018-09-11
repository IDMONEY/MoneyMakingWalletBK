#region Libraries
using System;
using System.Diagnostics;
#endregion

namespace IDMONEY.IO
{
    public static class Ensure
    {
        /// <summary>
        /// Determines whether the given argument is not null.
        /// </summary>
        /// <param name="parameter">The value.</param>
        [DebuggerStepThrough]
        public static void IsNotNull(object parameter)
        {
            if (parameter.IsNull())
            {
                throw new ArgumentNullException(nameof(parameter));
            }
        }

        /// <summary>
        /// Determines whether the given <see cref="String"/> is null, empty or contains only white spaces.
        /// </summary>
        /// <param name="parameter">The string value.</param>
        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty(string parameter)
        {
            if (String.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be null or empty");
            }
        }
    }
}