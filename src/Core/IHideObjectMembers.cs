#region Libraries
using System;
using System.ComponentModel; 
#endregion

namespace IDMONEY.IO
{
    /// <summary>
    /// Hides the common object members in development environment.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembers
    {
        /// <summary>
        /// Gets the <see cref="Type"/> of the current instance.
        /// </summary>
        /// <returns>The <see cref="Type"/> instance that represents the exact runtime type of the current instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Object"/>.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current <see cref="Object"/>.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Object"/>.</param>
        /// <returns>true if the specified <see cref="Object"/> is equal to the current <see cref="Object"/>; otherwise, false.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}