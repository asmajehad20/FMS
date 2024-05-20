using FleetMSLogic.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Repository
{
    /// <summary>
    /// Represents a repository for managing Drivers in the database.
    /// </summary>
    public class DriverRepository
    {
        private readonly DatabaseConnection dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverRepository"/> class.
        /// </summary>
        /// <remarks>Creates a new instance of <see cref="DriverRepository"/> and initializes the database connection.</remarks>
        public DriverRepository() 
        {
            dbConnection = new DatabaseConnection();
        }


        /// <summary>
        /// Adds a new driver to the database.
        /// </summary>
        /// <param name="driver">The <see cref="Driver"/> object representing the driver to be added.</param>
        /// <returns>True if the driver is successfully added to the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the driver to the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to insert a new driver
        /// into the "Driver" table. The driver's name and phone number are extracted from the provided <paramref name="driver"/> object.
        /// If the operation is successful, the method returns true with a success message printed to the console. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool AddDriver(Driver driver)
        {
            try
            {
                dbConnection.OpenConnection();
                ///////////////////////////////////////

                string sql = "INSERT INTO Driver (DriverName, PhoneNumber) VALUES (@DriverName, @PhoneNumber)";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@DriverName", driver.DriverName);
                command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);
                command.ExecuteNonQuery();

                Console.WriteLine($"driver added successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to add driver {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Checks if a driver with the specified driver ID exists in the database.
        /// </summary>
        /// <param name="driverID">The ID of the driver to check for existence.</param>
        /// <returns>True if a driver with the specified ID exists in the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while checking for the existence of the driver in the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to count the number of drivers
        /// with the specified driver ID in the "Driver" table. If the count is greater than zero, it means the driver exists,
        /// and the method returns true. Otherwise, it returns false. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool DriverExist(string driverID)
        {
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT COUNT(*) FROM Driver WHERE DriverID = @DriverID";

                using NpgsqlCommand Command = new(sql, dbConnection.Connection);
                Command.Parameters.AddWithValue("@DriverID", long.Parse(driverID));
                long count = (long?)Command.ExecuteScalar() ?? 0;

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Updates the information of an existing driver in the database.
        /// </summary>
        /// <param name="driver">The <see cref="Driver"/> object containing the updated information of the driver.</param>
        /// <returns>True if the driver information is successfully updated in the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the driver information in the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to update the information of the driver
        /// in the "Driver" table based on the provided <paramref name="driver"/> object. The driver's name and phone number are updated
        /// using the values from the <paramref name="driver"/> object, and the update is performed based on the driver's ID.
        /// If the operation is successful, the method returns true with a success message printed to the console. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool UpdateDriver(Driver driver)
        {
            try
            {
                dbConnection.OpenConnection();

                string sql = "UPDATE Driver SET DriverName = @DriverName, PhoneNumber = @PhoneNumber WHERE DriverID = @DriverID";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@DriverName", driver.DriverName);
                command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);
                command.Parameters.AddWithValue("@DriverID", driver.DriverID);
                command.ExecuteNonQuery();

                Console.WriteLine($"driver updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to update driver {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Deletes a driver from the database based on the specified driver ID.
        /// </summary>
        /// <param name="driverID">The ID of the driver to be deleted from the database.</param>
        /// <returns>True if the driver is successfully deleted from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the driver from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to delete the driver
        /// from the "Driver" table based on the provided driver ID. If the operation is successful, the method returns true with a success message printed to the console.
        /// If an exception occurs during the process, the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool DeleteDriver(string driverID)
        {
            try
            {
                dbConnection.OpenConnection();

                ////////////////////////////////////

                string sql = "DELETE FROM Driver WHERE DriverID = @DriverID";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@DriverID", long.Parse(driverID));
                command.ExecuteNonQuery();

                Console.WriteLine($"driver Deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to delete driver {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about a driver from the database based on the specified driver ID.
        /// </summary>
        /// <param name="driverID">The ID of the driver to retrieve information for.</param>
        /// <returns>
        /// A <see cref="Driver"/> object containing the information about the driver if found in the database; otherwise, null.
        /// </returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving information about the driver from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve the name and phone number
        /// of the driver with the specified driver ID from the "Driver" table. If a record matching the ID is found, a new <see cref="Driver"/> object
        /// is created with the retrieved information and returned. If no record is found, null is returned. If an exception occurs during the process,
        /// the method returns null with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public Driver? GetDriver(string driverID)
        {
            try
            {

                dbConnection.OpenConnection();

                string sql = "SELECT DriverName, PhoneNumber  FROM Driver WHERE DriverID = @DriverID";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@DriverID", long.Parse(driverID));

                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Driver driver = new()
                    {
                        DriverID = long.Parse(driverID),
                        DriverName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        PhoneNumber = reader.IsDBNull(1) ? 0 : reader.GetInt64(1),
                    };

                    return driver;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about all drivers from the database.
        /// </summary>
        /// <param name="drivers">Output parameter to store the list of <see cref="Driver"/> objects containing information about all drivers.</param>
        /// <returns>
        /// True if the information about all drivers is successfully retrieved from the database; otherwise, false.
        /// </returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving information about the drivers from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve the ID, name, and phone number
        /// of all drivers from the "Driver" table. For each record retrieved, a new <see cref="Driver"/> object is created
        /// with the retrieved information and added to the output list of drivers. If the operation is successful, the method returns true.
        /// If an exception occurs during the process, the method returns false with an error message printed to the console. 
        /// Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllDrivers(out List<Driver> drivers)
        {
            drivers = [];
            try
            {
                dbConnection.OpenConnection();
                string sql1 = "SELECT DriverID, DriverName, PhoneNumber " +
                    "FROM Driver ORDER BY DriverID;";

                using NpgsqlCommand command1 = new(sql1, dbConnection.Connection);
                using NpgsqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    Driver driver = new()
                    {
                        DriverID = reader1.IsDBNull(0) ? 0 : reader1.GetInt64(0),
                        DriverName = reader1.IsDBNull(1) ? string.Empty : reader1.GetString(1),
                        PhoneNumber = reader1.IsDBNull(2) ? 0 : reader1.GetInt64(2),
                    };

                    drivers.Add(driver);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
        
    }
}