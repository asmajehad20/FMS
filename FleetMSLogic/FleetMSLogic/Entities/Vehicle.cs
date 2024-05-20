
namespace FleetMSLogic.Entities
{
    public class Vehicle
    {
        public long VehicleID { get; set; }
        public long VehicleNumber { get; set; }
        public string VehicleType { get; set; } = string.Empty;

        public long DriverID { get; set; }
        public string VehicleMake { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public long PurchaseDate { get; set; }

        public int? LastDirection { get; set; }
        public char LastStatus { get; set; }
        public string LastAddress { get; set; } = string.Empty;
        public double LastLatitude { get; set; }
        public double LastLongitude { get; set; }

        public long LastGPSTime {  get; set; }
        public string LastGPSSpeed { get; set; } = string.Empty;
    }
}
