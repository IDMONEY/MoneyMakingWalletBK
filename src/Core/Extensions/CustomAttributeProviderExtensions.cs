#region Libraries
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
#endregion

namespace IDMONEY.IO
{
    /// <summary>
    /// Static class which contains extension methods of <see cref="ICustomAttributeProvider"/>.
    /// </summary>
    public static class CustomAttributeProviderExtensions
    {
        /// <summary>
        /// Retunrs an <see cref="IEnumerable{TAttibute}"/> of the custom attributes of the given type defined on this member,
        /// excluding the named attributes.
        /// </summary>
        /// <typeparam name="TAttribute">A generic parameter that specifies the type of the custom attribute.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
        /// <returns>An <see cref="IEnumerable{TAttibute}"/> with the custom attributes of the given type, or an empty <see cref="IEnumerable{TAttibute}"/>.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider instance, bool inherit)
        {
            Ensure.IsNotNull(instance);
            return instance.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>();
        }

    }
}