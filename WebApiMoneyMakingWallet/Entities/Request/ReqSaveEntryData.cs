using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class ReqSaveEntryData : BaseRequest
    {
        public List<DataEntry> DataEntries { get; set; }
    }
}
