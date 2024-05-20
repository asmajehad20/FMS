using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Entities
{
    public class Geofences
    {
        public long GeofenceID { get; set; }
        public string GeofenceType { get; set; } = string.Empty;
        public long AddedDate { get; set; }
        public string StrokeColor { get; set; } = string.Empty;
        public double StrokeOpacity { get; set; }
        public double StrokeWeight { get; set; }
        public string FillColor { get; set; } = string.Empty;
        public double FillOpacity { get; set; }

    }
}
