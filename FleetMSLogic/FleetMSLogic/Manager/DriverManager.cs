using FleetMSLogic.Entities;
using FPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetMSLogic.Repository;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace FleetMSLogic.Manager
{
    public enum DriverTags
    {
        DriverID,
        DriverName,
        PhoneNumber,
    }

    public class DriverManager
    {
        private readonly DriverRepository _driverRepository;
        public DriverManager() 
        {
            _driverRepository = new DriverRepository();
        }

        public GVAR GetAllDrivers()
        {
            //get data from repository;
            if (!_driverRepository.GetAllDrivers(out List<Driver> drivers))
            {
                throw new Exception($"Failed to get drivers");
            }

            //conver them to dataTable
            //[Serializable]
            var Drivers = new DataTable();

            Drivers.Columns.Add(DriverTags.DriverID.ToString());
            Drivers.Columns.Add(DriverTags.DriverName.ToString());
            Drivers.Columns.Add(DriverTags.PhoneNumber.ToString());

            foreach (var driver in drivers)
            {
                DataRow row = Drivers.NewRow();
                row[DriverTags.DriverID.ToString()] = driver.DriverID;
                row[DriverTags.DriverName.ToString()] = driver.DriverName;
                row[DriverTags.PhoneNumber.ToString()] = driver.PhoneNumber;

                Drivers.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT[nameof(Drivers)] = Drivers;

            return Gvar;
        }

        public void AddDriver(GVAR Gvar)
        {

            if (!Gvar.DicOfDic["Tags"].ContainsKey(DriverTags.DriverName.ToString()) ||
                !Gvar.DicOfDic["Tags"].ContainsKey(DriverTags.PhoneNumber.ToString()))
            {
                throw new ArgumentException($"{DriverTags.DriverName} and {DriverTags.PhoneNumber} are required fields.");
            }

            //inputs are null or empty
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][DriverTags.DriverName.ToString()]))
            {
                throw new ArgumentException($"Invalid {DriverTags.DriverName}. It cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][DriverTags.PhoneNumber.ToString()]))
            {
                throw new ArgumentException($"Invalid {DriverTags.PhoneNumber}. It cannot be null or empty.");
            }

            //invalid vehicle number
            if (!long.TryParse(Gvar.DicOfDic["Tags"][DriverTags.PhoneNumber.ToString()], out long phoneNumber))
            {
                throw new ArgumentException($"Invalid {DriverTags.PhoneNumber}.");
            }

            Driver driver = new()
            {
                DriverName = Gvar.DicOfDic["Tags"][DriverTags.DriverName.ToString()],
                PhoneNumber = phoneNumber

            };

            if (!_driverRepository.AddDriver(driver))
            {
                throw new Exception($"failed to add Vehicle");
            }
        }

        public void UpdateDriver(string DriverID, GVAR Gvar)
        {
            if (Gvar == null || string.IsNullOrEmpty(DriverID))
            {
                throw new ArgumentNullException($"{nameof(Gvar)} and {nameof(DriverID)} cannot be null.");
            }

            //check input format
            if (!Gvar.DicOfDic.ContainsKey("Tags"))
            {
                throw new ArgumentException("Tags dictionary is missing in GVAR object.");
            }

            if (!long.TryParse(DriverID.ToString(), out _))
            {
                throw new ArgumentException("invalid DriverID.");
            }

            if (!_driverRepository.DriverExist(DriverID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            Driver? storedDriver = _driverRepository.GetDriver(DriverID) ?? throw new Exception($"failed to retrieve driver");

            if (Gvar.DicOfDic["Tags"].ContainsKey(DriverTags.PhoneNumber.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][DriverTags.PhoneNumber.ToString()]) &&
                long.TryParse(Gvar.DicOfDic["Tags"][DriverTags.PhoneNumber.ToString()], out long phoneNumber)
                )
            {
                storedDriver.PhoneNumber = phoneNumber;
            }

            if (Gvar.DicOfDic["Tags"].ContainsKey(DriverTags.DriverName.ToString()) &&
                !string.IsNullOrEmpty(Gvar.DicOfDic["Tags"][DriverTags.DriverName.ToString()])
                )
            {
                storedDriver.DriverName = Gvar.DicOfDic["Tags"][DriverTags.DriverName.ToString()];
            }

            if (!_driverRepository.UpdateDriver(storedDriver))
            {
                throw new Exception($"failed to update");
            }

        }

        public void DeleteDriver(string DriverID)
        {
            if (string.IsNullOrEmpty(DriverID))
            {
                throw new ArgumentNullException($"{nameof(DriverID)} cannot be null.");
            }

            if (!long.TryParse(DriverID.ToString(), out _))
            {
                throw new ArgumentException("invalid DriverID.");
            }

            if (!_driverRepository.DriverExist(DriverID))
            {
                throw new Exception($"Vehicle does not Exist");
            }

            if (!_driverRepository.DeleteDriver(DriverID))
            {
                throw new Exception($"failed to delete");
            }

        }

    }
}
