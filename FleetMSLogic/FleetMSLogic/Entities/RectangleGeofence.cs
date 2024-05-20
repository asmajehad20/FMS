using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Entities
{
    public class RectangleGeofence
    {
        public long GeofenceID { get; set; }
        public double North { get; set; }
        public double East { get; set; }
        public double West { get; set; }
        public double South { get; set; }
    }
}
