using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Entities
{
    public class Driver
    {
        public long DriverID { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }

    }
}
