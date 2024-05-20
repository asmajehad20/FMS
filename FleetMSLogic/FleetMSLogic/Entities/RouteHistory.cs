using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Entities
{
    public class RouteHistory
    {
        //public long RouteHistoryID { get; set; }
        public long VehicleID { get; set; }
        public int VehicleDirection { get; set; }
        public char Status { get; set; } 
        public string VehicleSpeed { get; set; } = string.Empty;
        public long Epoch { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
