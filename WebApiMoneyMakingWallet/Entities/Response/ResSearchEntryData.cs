using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ResSearchEntryData : BaseResponse
    {
        public List<DataEntry> DataEntries { get; set; }
    }
}
