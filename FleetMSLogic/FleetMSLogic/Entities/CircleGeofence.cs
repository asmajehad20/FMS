using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Entities
{
    public class CircleGeofence
    {
        public long GeofenceID { get; set; }
        public long Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
