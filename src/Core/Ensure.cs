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

        /// <summary>
        /// Determines whether the given argument is not negative or zero.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name</param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(long parameter)
        {
            if (parameter <= 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative or equals to zero");
            }
        }

        /// <summary>
        /// Determines whether the given argument is not negative.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        [DebuggerStepThrough]
        public static void IsNotNegative(long parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative");
            }
        }

        /// <summary>
        /// Determines whether the given argument is not negative or zero.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name</param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(float parameter, string parameterName)
        {
            if (parameter <= 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative or equals to zero");
            }
        }

        /// <summary>
        /// Determines whether the given argument is not negative.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        [DebuggerStepThrough]
        public static void IsNotNegative(float parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative");
            }
        }

        /// <summary>
        /// Determines whether the given argument is not negative or zero.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name</param>
        [DebuggerStepThrough]
        public static void IsNotNegativeOrZero(double parameter, string parameterName)
        {
            if (parameter <= 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative or equals to zero");
            }
        }

        /// <summary>
        /// Determines whether the given argument is not negative.
        /// </summary>
        /// <param name="parameter">The numeric value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        [DebuggerStepThrough]
        public static void IsNotNegative(double parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentException($"{nameof(parameter)} cannot be negative");
            }
        }

        /// <summary>
        /// Determines whether the given value is defined in the specific enum.
        /// </summary>
        /// <param name="value">Enum's value</param>
        [DebuggerStepThrough]
        public static void IsEnumDefined<T>(object value) where T : struct
        {
            Type type = typeof(T);
            if (!Enum.IsDefined(type, value))
            {
                throw new ArgumentException($"The {value} is an invalid value for the enum {type}");
            }
        }
    }
}