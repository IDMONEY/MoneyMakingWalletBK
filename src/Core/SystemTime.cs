#region Libraries
using System;
using System.Diagnostics; 
#endregion

namespace IDMONEY.IO
{
    /// <summary>
    /// Class that access the system current date and time.
    /// <remarks>This class must be used instead of <see cref="DateTime"/> becuase it makes unit testing of time-sensitive code far easier.</remarks>
    /// </summary>
    public static class SystemTime
    {
        private static Func<DateTime> now = () => DateTime.UtcNow;

        /// <summary>
        /// Returns the current date and time.
        /// <remarks>
        /// This is an alias for DateTime.Now; however, given that it's writable,
        /// it makes unit testing of time-sensitive code far easier.
        /// </remarks>
        /// </summary>
        public static Func<DateTime> Now
        {
            [DebuggerStepThrough]
            get
            { return now; }
            [DebuggerStepThrough]
            set
            { now = value; }
        }

        /// <summary>
        /// Resets the current date and time value to its default.
        /// <remarks>(The default value is DateTime.Now)</remarks>
        /// </summary>
        public static void ResetNow()
        {
            now = () => DateTime.UtcNow;
        }
    }

}
