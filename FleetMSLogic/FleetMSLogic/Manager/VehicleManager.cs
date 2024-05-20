using FleetMSLogic.Repository;
using FleetMSLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPro;
using System.Collections;
using System.Data;
using System.Xml.Serialization;

namespace FleetMSLogic.Manager
{
    public enum VehicleTags
    {
        VehicleID,
        VehicleNumber,
        VehicleType,

        DriverID,
        VehicleMake,
        VehicleModel,
        PurchaseDate,

        LastDirection,
        LastStatus,
        LastAddress,
        LastPosition,

        LastGPSTime,
        LastGPSSpeed,

    }

    public enum RouteHistoryTags
    {
        Address,
        Status,
        Latitude,
        Longitude,
        VehicleDirection,
        GPSSpeed,
        VehicleSpeed,
        GPSTime,
        Epoch
    }

    public class VehicleManager
    {
        private readonly VehicleRepository _vehicleRepository;
        public VehicleManager()
        {
            _vehicleRepository = new VehicleRepository();
        }

        public GVAR GetAllVehicles()
        {
            //get data from repository;
            if (!_vehicleRepository.GetAllVehicles(out List<Vehicle> vehicles))
            {
                throw new Exception($"Failed to get vehicles");
            }
            
            //conver them to dataTable
            var vehicleDataTable = new DataTable();

            vehicleDataTable.Columns.Add(VehicleTags.VehicleID.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.VehicleNumber.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.VehicleType.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.LastDirection.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.LastStatus.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.LastAddress.ToString());
            vehicleDataTable.Columns.Add(VehicleTags.LastPosition.ToString());

            foreach (var vehicle in vehicles)
            {
                DataRow row = vehicleDataTable.NewRow();
                row[VehicleTags.VehicleID.ToString()] = vehicle.VehicleID;
                row[VehicleTags.VehicleNumber.ToString()] = vehicle.VehicleNumber;
                row[VehicleTags.VehicleType.ToString()] = vehicle.VehicleType;
                row[VehicleTags.LastDirection.ToString()] = vehicle.LastDirection;
                row[VehicleTags.LastStatus.ToString()] = vehicle.LastStatus;
                row[VehicleTags.LastAddress.ToString()] = vehicle.LastAddress;
                row[VehicleTags.LastPosition.ToString()] = vehicle.LastLatitude+", "+vehicle.LastLongitude; 
                
                vehicleDataTable.Rows.Add(row);
            }
 
            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT["Vehicles"] = vehicleDataTable;
            return Gvar;
        }

        public GVAR GetVehicleInformations(string vehicleID)
        {
            //if vehicle exist
            if(!_vehicleRepository.VehicleExist(vehicleID))
            {
                throw new Exception($"Vehicle does not exist");
            }
            //get data from repository;
            if (!_vehicleRepository.GettheVehicleInformations(vehicleID, out Vehicle vehicle, out Driver driver))
            {
                throw new Exception($"Failed to get vehicle informations2");
            }

            //conver them to dataTable
            var vehicleInformationDataTable = new DataTable();
            vehicleInformationDataTable.Columns.Add(VehicleTags.VehicleNumber.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.VehicleType.ToString());
            vehicleInformationDataTable.Columns.Add(DriverTags.DriverName.ToString());
            vehicleInformationDataTable.Columns.Add(DriverTags.PhoneNumber.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.LastPosition.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.VehicleMake.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.VehicleModel.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.LastGPSTime.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.LastGPSSpeed.ToString());
            vehicleInformationDataTable.Columns.Add(VehicleTags.LastAddress.ToString());

            DataRow row = vehicleInformationDataTable.NewRow();
            row[VehicleTags.VehicleNumber.ToString()] = vehicle.VehicleNumber;
            row[VehicleTags.VehicleType.ToString()] = vehicle.VehicleType;
            row[DriverTags.DriverName.ToString()] = driver.DriverName;
            row[DriverTags.PhoneNumber.ToString()] = driver.PhoneNumber;
            row[VehicleTags.LastPosition.ToString()] = vehicle.LastLatitude + ", " + vehicle.LastLongitude;
            row[VehicleTags.VehicleMake.ToString()] = vehicle.VehicleMake;
            row[VehicleTags.VehicleModel.ToString()] = vehicle.VehicleModel;
            row[VehicleTags.LastGPSTime.ToString()] = vehicle.LastGPSTime;
            row[VehicleTags.LastGPSSpeed.ToString()] = vehicle.LastGPSSpeed;
            row[VehicleTags.LastAddress.ToString()] = vehicle.LastAddress;
            vehicleInformationDataTable.Rows.Add(row);

            GVAR Gvar = new();
            Gvar.DicOfDT["VehicleInformation"] = vehicleInformationDataTable;
            return Gvar;
        }

        public GVAR GetVehicleRouteHistory(string VehicleID, string StartTime, string EndTime)
        {
            //if vehicle exist
            if (!_vehicleRepository.VehicleExist(VehicleID))
            {
                throw new Exception($"Vehicle does not exist");
            }

            if(!long.TryParse(StartTime, out _))
            {
                throw new ArgumentException($"Invalid {nameof(StartTime)}.");
            }

            if (!long.TryParse(EndTime, out _))
            {
                throw new ArgumentException($"Invalid {nameof(EndTime)}.");
            }

            if (!_vehicleRepository.GetVehicleRouteHistory(VehicleID, StartTime, EndTime, out Vehicle vehicle, out List<RouteHistory> histories))
            {
                throw new Exception($"Failed to get Route History");
            }

            //conver to dataTable
            var RouteHistory = new DataTable();
            RouteHistory.Columns.Add(VehicleTags.VehicleID.ToString());
            RouteHistory.Columns.Add(VehicleTags.VehicleNumber.ToString());

            RouteHistory.Columns.Add(RouteHistoryTags.Address.ToString());
            RouteHistory.Columns.Add(RouteHistoryTags.Status.ToString());

            RouteHistory.Columns.Add(RouteHistoryTags.Latitude.ToString());
            RouteHistory.Columns.Add(RouteHistoryTags.Longitude.ToString());
            RouteHistory.Columns.Add(RouteHistoryTags.VehicleDirection.ToString());

            RouteHistory.Columns.Add(RouteHistoryTags.GPSSpeed.ToString());
            RouteHistory.Columns.Add(RouteHistoryTags.GPSTime.ToString());

            foreach (var history in histories)
            {
                DataRow row = RouteHistory.NewRow();
                row[VehicleTags.VehicleID.ToString()] = vehicle.VehicleID;
                row[VehicleTags.VehicleNumber.ToString()] = vehicle.VehicleNumber;

                row[RouteHistoryTags.Address.ToString()] = history.Address;
                row[RouteHistoryTags.Status.ToString()] = history.Status;

                row[RouteHistoryTags.Latitude.ToString()] = history.Latitude;
                row[RouteHistoryTags.Longitude.ToString()] = history.Longitude;
                row[RouteHistoryTags.VehicleDirection.ToString()] = history.VehicleDirection;

                row[RouteHistoryTags.GPSSpeed.ToString()] = history.VehicleSpeed;
                row[RouteHistoryTags.GPSTime.ToString()] = history.Epoch;
                RouteHistory.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT[nameof(RouteHistory)] = RouteHistory;
            return Gvar;
        }

        public void AddVehicle(GVAR  Gvar)
        {

            if (!Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleNumber.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleType.ToString()))
            {
                throw new ArgumentException($"{nameof(VehicleTags.VehicleNumber)} and {nameof(VehicleTags.VehicleType)} are required fields.");
            }

            //inputs are null or empty
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleType.ToString()]))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.VehicleType)}. It cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleNumber.ToString()]))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.VehicleNumber)}. It cannot be null or empty.");
            }

            //invalid vehicle number
            if (!long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.VehicleNumber.ToString()], out long vehicleNumber))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.VehicleNumber)}.");
            }

            Vehicle vehicle = new()
            {
                VehicleNumber = vehicleNumber,
                VehicleType = Gvar.DicOfDic["Tags"][VehicleTags.VehicleType.ToString()]

            };

            if (!_vehicleRepository.AddVehicle(vehicle))
            {
                throw new Exception($"failed to add Vehicle");
            }
        }

        public void AddVehicleInformations(GVAR Gvar) 
        {

            if (!Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleID.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.DriverID.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleMake.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleModel.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.PurchaseDate.ToString()) 
                ) 
            {
                throw new ArgumentException(
                    $"{nameof(VehicleTags.VehicleID)}, {nameof(VehicleTags.DriverID)}, " +
                    $"{nameof(VehicleTags.VehicleMake)}, {nameof(VehicleTags.VehicleModel)} " +
                    $"and {nameof(VehicleTags.PurchaseDate)} are required fields.");
            }

            //input have null or empty value
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleID.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleMake.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleModel.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.PurchaseDate.ToString()])
                )
            {
                throw new ArgumentException($"required fields cannot be null or empty.");
            }

            //invalid vehicleID
            if (!long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.VehicleID.ToString()], out long vehicleID))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.VehicleID)}.");
            }

            //invalid driverID
            long driverID=0;
            if (!string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.DriverID.ToString()]) && !long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.DriverID.ToString()], out driverID))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.DriverID)}.");
            }

            //invalid purchaseDate
            if (!long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.PurchaseDate.ToString()], out long purchaseDate))
            {
                throw new ArgumentException($"Invalid {nameof(VehicleTags.PurchaseDate)}.");
            }

            Vehicle vehicle = new()
            {
                VehicleID = vehicleID,
                DriverID = driverID,
                VehicleMake = Gvar.DicOfDic["Tags"][VehicleTags.VehicleMake.ToString()],
                VehicleModel = Gvar.DicOfDic["Tags"][VehicleTags.VehicleModel.ToString()],
                PurchaseDate = purchaseDate,

            };

            if (!_vehicleRepository.AddVehicleInformations(vehicle))
            {
                throw new Exception($"failed to add Vehicle information");
            }

        }

        public void AddRouteHistory(GVAR Gvar)
        {
            
            if (!Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleID.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.VehicleDirection.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.Status.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.Epoch.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.VehicleSpeed.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.Address.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.Latitude.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(RouteHistoryTags.Longitude.ToString())
                )
            {
                throw new ArgumentException(
                    $"{nameof(VehicleTags.VehicleID)}, {nameof(RouteHistoryTags.VehicleSpeed)}, " +
                    $"{nameof(RouteHistoryTags.VehicleDirection)}, {nameof(RouteHistoryTags.Epoch)}, " +
                    $"{nameof(RouteHistoryTags.Status)}, {nameof(RouteHistoryTags.Address)}, " +
                    $"{nameof(RouteHistoryTags.Latitude)}, {nameof(RouteHistoryTags.Longitude)}" +
                    $"are required fields.");
            }

            //validate inputs 
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleID.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.VehicleDirection.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.Status.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.Epoch.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.VehicleSpeed.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.Address.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.Latitude.ToString()]) ||
                string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][RouteHistoryTags.Longitude.ToString()])
                )
            {
                throw new ArgumentException($"Invalid inputs. can not be null or empty.");
            }

            if (!int.TryParse(Gvar.DicOfDic["Tags"][RouteHistoryTags.VehicleDirection.ToString()], out int vehicleDirection))
            {
                throw new ArgumentException($"invalid {nameof(RouteHistoryTags.VehicleDirection)}, should be int");
            }
            if (!double.TryParse(Gvar.DicOfDic["Tags"][RouteHistoryTags.Latitude.ToString()], out double latitude))
            {
                throw new ArgumentException($"invalid {nameof(RouteHistoryTags.Latitude)}, should be double");
            }
            if (!double.TryParse(Gvar.DicOfDic["Tags"][RouteHistoryTags.Longitude.ToString()], out double longitude))
            {
                throw new ArgumentException($"invalid {nameof(RouteHistoryTags.Longitude)}, should be double");
            }
            if (!long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.VehicleID.ToString()], out long vehicleID))
            {
                throw new ArgumentException($"invalid {nameof(VehicleTags.VehicleID)}, should be long");
            }
            if (!long.TryParse(Gvar.DicOfDic["Tags"][RouteHistoryTags.Epoch.ToString()], out long epoch))
            {
                throw new ArgumentException($"invalid {nameof(RouteHistoryTags.Epoch)}, should be long");
            }
            if (!char.TryParse(Gvar.DicOfDic["Tags"][RouteHistoryTags.Status.ToString()], out char status))
            {
                throw new ArgumentException($"invalid {nameof(RouteHistoryTags.Status)}, should be char");
            }

            RouteHistory history = new()
            {
                VehicleID = vehicleID,
                Latitude = latitude,
                Longitude = longitude,
                Epoch = epoch,
                VehicleDirection = vehicleDirection,
                Address = Gvar.DicOfDic["Tags"][RouteHistoryTags.Address.ToString()],
                VehicleSpeed = Gvar.DicOfDic["Tags"][RouteHistoryTags.VehicleSpeed.ToString()],
                Status = status
            };
            //play the function
            if (!_vehicleRepository.AddRouteHistory(history))
            {
                throw new Exception($"Failed to add route history");
            }
        }

        public void UpdateVehicle(string VehicleID, GVAR Gvar)
        {
            if (Gvar == null || string.IsNullOrEmpty(VehicleID))
            {
                throw new ArgumentNullException($"{nameof(Gvar)} and {nameof(VehicleID)} cannot be null.");
            }

            if(!long.TryParse(VehicleID.ToString(), out _))
            {
                throw new ArgumentException("invalid VehicleID.");
            }

            if (!_vehicleRepository.VehicleExist(VehicleID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            Vehicle? stroedVehicle = _vehicleRepository.GetVehicle(VehicleID) ?? throw new Exception($"failed to retrieve vehicle");

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleNumber.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleNumber.ToString()]) &&
                long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.VehicleNumber.ToString()], out long vehicleNumber)
                )
            {
                stroedVehicle.VehicleNumber = vehicleNumber;
            }

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleType.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleType.ToString()]) 
                )
            {
                stroedVehicle.VehicleType = Gvar.DicOfDic["Tags"][VehicleTags.VehicleType.ToString()];
            }

            if (!_vehicleRepository.UpdateVehicle(stroedVehicle))
            {
                throw new Exception($"failed to update");
            }
            
        }

        public void UpdateVehicleInformations(string VehicleID, GVAR Gvar)
        {
            if (Gvar == null || string.IsNullOrEmpty(VehicleID))
            {
                throw new ArgumentNullException($"{nameof(Gvar)} and {nameof(VehicleID)} cannot be null.");
            }

            //check input format
            if (!Gvar.DicOfDic.ContainsKey("Tags"))
            {
                throw new ArgumentException("Tags dictionary is missing in GVAR object.");
            }

            if (!long.TryParse(VehicleID.ToString(), out _))
            {
                throw new ArgumentException("invalid VehicleID.");
            }

            if (!_vehicleRepository.VehicleExist(VehicleID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            Vehicle? stroedVehicleInfo = _vehicleRepository.GetVehicleInformations(VehicleID) ?? addEmptyInformation(VehicleID, Gvar.DicOfDic["Tags"][VehicleTags.DriverID.ToString()]) ;

            if (stroedVehicleInfo == null) { throw new Exception(""); }

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.PurchaseDate.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.PurchaseDate.ToString()]) &&
                long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.PurchaseDate.ToString()], out long purchaseDate)
                )
            {
                stroedVehicleInfo.PurchaseDate = purchaseDate;
            }

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleMake.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleMake.ToString()])
                )
            {
                stroedVehicleInfo.VehicleMake = Gvar.DicOfDic["Tags"][VehicleTags.VehicleMake.ToString()];
            }

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.VehicleModel.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.VehicleModel.ToString()])
                )
            {
                stroedVehicleInfo.VehicleModel = Gvar.DicOfDic["Tags"][VehicleTags.VehicleModel.ToString()];
            }

            if (Gvar.DicOfDic["Tags"].ContainsKey(VehicleTags.DriverID.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][VehicleTags.DriverID.ToString()]) &&
                long.TryParse(Gvar.DicOfDic["Tags"][VehicleTags.DriverID.ToString()], out long driverid)
                )
            {
                stroedVehicleInfo.DriverID = driverid;
            }

            if (!_vehicleRepository.UpdateVehicleInformations(stroedVehicleInfo))
            {
                throw new Exception($"failed to update");
            }

        }

        private Vehicle addEmptyInformation(string VehicleID, string Driverid)
        {
            Vehicle vehicle = new()
            {
                VehicleID = long.Parse(VehicleID),
                DriverID = long.Parse(Driverid),
                VehicleMake = string.Empty,
                VehicleModel = string.Empty,
                PurchaseDate = 0,

            };

            if (!_vehicleRepository.AddVehicleInformations(vehicle))
            {
                throw new Exception($"failed to add Vehicle information");
            }

            return vehicle;

        }

        public void DeleteVehicle(string VehicleID)
        {
            if (string.IsNullOrEmpty(VehicleID))
            {
                throw new ArgumentNullException($"{nameof(VehicleID)} cannot be null.");
            }

            if (!long.TryParse(VehicleID.ToString(), out _))
            {
                throw new ArgumentException("invalid VehicleID.");
            }

            if (!_vehicleRepository.VehicleExist(VehicleID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            if (!_vehicleRepository.DeleteVehicle(VehicleID))
            {
                throw new Exception($"failed to delete");
            }

        }

        public void DeleteVehicleInformations(string VehicleID)
        {
            if (string.IsNullOrEmpty(VehicleID))
            {
                throw new ArgumentNullException($"{nameof(VehicleID)} cannot be null.");
            }

            if (!long.TryParse(VehicleID.ToString(), out _))
            {
                throw new ArgumentException("invalid VehicleID.");
            }

            if (!_vehicleRepository.VehicleExist(VehicleID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            if (!_vehicleRepository.DeleteVehicleInformations(VehicleID))
            {
                throw new Exception($"failed to delete");
            }

        }

    }
}