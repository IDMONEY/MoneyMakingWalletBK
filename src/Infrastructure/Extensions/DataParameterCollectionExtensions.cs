#region Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text; 
#endregion

namespace IDMONEY.IO
{
    public static class DataParameterCollectionExtensions
    {
        [DebuggerStepThrough]
        public static void AddRange(this IDataParameterCollection collection, IEnumerable<IDataParameter> parameters)
        {
            Ensure.IsNotNull(collection);
            Ensure.IsNotNull(parameters);

            foreach (IDataParameter item in parameters)
            {
                collection.Add(item);
            }
        }
    }
}