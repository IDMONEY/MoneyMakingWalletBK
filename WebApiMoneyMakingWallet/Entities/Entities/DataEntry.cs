using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDMONEY.IO.Entities
{
    public class DataEntry
    {
        public int SettingId { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}
