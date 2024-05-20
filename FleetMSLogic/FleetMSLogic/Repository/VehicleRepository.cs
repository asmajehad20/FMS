
using FleetMSLogic.Entities;
using System.Collections.Generic;
using Npgsql;
using System.Net.NetworkInformation;
using System.Net;

namespace FleetMSLogic.Repository
{
    /// <summary>
    /// Represents a repository for managing vehicles in the database.
    /// </summary>
    public class VehicleRepository
    {
        private readonly DatabaseConnection dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <remarks>Creates a new instance of <see cref="VehicleRepository"/> and initializes the database connection.</remarks>
        public VehicleRepository() 
        {
            dbConnection = new DatabaseConnection();
        }

        /// <summary>
        /// Adds a new vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> object representing the vehicle to be added.</param>
        /// <returns>True if the vehicle is successfully added to the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the vehicle to the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, inserts the vehicle information into the "Vehicles" table,
        /// and then closes the database connection. If an exception occurs during the process, the method returns false,
        /// and an error message is printed to the console.
        /// </remarks>
        public bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                dbConnection.OpenConnection();

                ///////////////////////////////////////
                string sql = "INSERT INTO Vehicles(VehicleNumber, VehicleType) VALUES(@VehicleNumber, @VehicleType)";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@VehicleNumber", vehicle.VehicleNumber);
                command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);
                command.ExecuteNonQuery();
                /////////////////////////////////////

                Console.WriteLine($"Vehicle added successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to add vehicle {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Adds additional information about a vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> object containing the information to be added.</param>
        /// <returns>True if the vehicle information is successfully added to the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the vehicle information to the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, inserts the additional vehicle information into the "VehiclesInformations" table,
        /// and then closes the database connection. If an exception occurs during the process, the method returns false,
        /// and an error message is printed to the console.
        /// </remarks>
        public bool AddVehicleInformations(Vehicle vehicle)
        {
            try
            {
                dbConnection.OpenConnection();

                ///////////////////////////////////////
                string sql = "INSERT INTO VehiclesInformations(VehicleID, DriverID, VehicleMake, VehicleModel, PurchaseDate) " +
                         "VALUES((SELECT VehicleID FROM Vehicles WHERE VehicleID = @VehicleID), (SELECT DriverID FROM Driver WHERE DriverID = @DriverID), " +
                         "@VehicleMake, @VehicleModel, @PurchaseDate)";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);

                command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                command.Parameters.AddWithValue("@DriverID", vehicle.DriverID);
                command.Parameters.AddWithValue("@VehicleMake", vehicle.VehicleMake);
                command.Parameters.AddWithValue("@VehicleModel", vehicle.VehicleModel);
                command.Parameters.AddWithValue("@PurchaseDate", vehicle.PurchaseDate);
                command.ExecuteNonQuery();
                ///////////////////////////////////////

                Console.WriteLine($"Vehicle information added successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to add vehicle information {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Updates the information of a vehicle in the database.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> object containing the updated information.</param>
        /// <returns>True if the vehicle information is successfully updated in the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the vehicle information in the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, updates the vehicle information in the "Vehicles" table based on the provided VehicleID,
        /// and then closes the database connection. If an exception occurs during the process, the method returns false,
        /// and an error message is printed to the console.
        /// </remarks>
        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                dbConnection.OpenConnection();

                ////////////////////////////////////
                string  Sql = "UPDATE Vehicles SET VehicleNumber = @VehicleNumber, VehicleType = @VehicleType WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command = new(Sql, dbConnection.Connection);

                Command.Parameters.AddWithValue("@VehicleNumber", vehicle.VehicleNumber);
                Command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);
                Command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                Command.ExecuteNonQuery();

                ////////////////////////////////////

                Console.WriteLine($"updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update: {ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        /// <summary>
        /// Checks if a vehicle with the specified ID exists in the database.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle to check for existence.</param>
        /// <returns>True if a vehicle with the specified ID exists in the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while checking the existence of the vehicle in the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, executes a SQL query to count the number of vehicles with the provided VehicleID in the "Vehicles" table,
        /// and then closes the database connection. If an exception occurs during the process, the method returns false,
        /// and an error message is printed to the console.
        /// </remarks>
        public bool VehicleExist(string vehicleID)
        {
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT COUNT(*) FROM Vehicles WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command = new(sql, dbConnection.Connection);
                Command.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
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
        /// Updates the additional information of a vehicle in the database.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> object containing the updated information.</param>
        /// <returns>True if the vehicle information is successfully updated in the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the vehicle information in the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, updates the additional vehicle information in the "VehiclesInformations" table based on the provided VehicleID,
        /// and then closes the database connection. If an exception occurs during the process, the method returns false,
        /// and an error message is printed to the console.
        /// </remarks>
        public bool UpdateVehicleInformations(Vehicle vehicle)
        {
            try
            {
                dbConnection.OpenConnection();

                ////////////////////////////////////
                string Sql = "UPDATE VehiclesInformations SET VehicleMake = @VehicleMake, DriverID = @DriverID, " +
                                              "VehicleModel = @VehicleModel, PurchaseDate = @PurchaseDate " +
                                              "WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command = new(Sql, dbConnection.Connection);
                Command.Parameters.AddWithValue("@VehicleMake", vehicle.VehicleMake);
                Command.Parameters.AddWithValue("@VehicleModel", vehicle.VehicleModel);
                Command.Parameters.AddWithValue("@PurchaseDate", vehicle.PurchaseDate);
                Command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                Command.Parameters.AddWithValue("@DriverID", vehicle.DriverID);
                Command.ExecuteNonQuery();

                ////////////////////////////////////
                Console.WriteLine($"updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update: {ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        /// <summary>
        /// Deletes a vehicle and its associated information from the database.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle to be deleted.</param>
        /// <returns>True if the vehicle and its associated information are successfully deleted from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the vehicle and its associated information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, begins a transaction, and then executes SQL commands to delete the vehicle and its associated information
        /// from the "VehiclesInformations", "RouteHistory", and "Vehicles" tables based on the provided VehicleID. If all operations are successful, the transaction is committed,
        /// and the method returns true. If an exception occurs during the process, the transaction is rolled back, and the method returns false with an error message printed to the console.
        /// Finally, the database connection is closed.
        /// </remarks>
        public bool DeleteVehicle(string VehicleID)
        {
            NpgsqlTransaction? transaction = null;

            try
            {
                dbConnection.OpenConnection();
                transaction = dbConnection.Connection.BeginTransaction();

                ////////////////////////////////////
                string Sql = "DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command = new(Sql, dbConnection.Connection);
                Command.Parameters.AddWithValue("@VehicleID", long.Parse(VehicleID));
                Command.ExecuteNonQuery();

                ////////////////////////////////////
                string Sql2 = "DELETE FROM RouteHistory WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command2 = new(Sql2, dbConnection.Connection);
                Command2.Parameters.AddWithValue("@VehicleID", long.Parse(VehicleID));
                Command2.ExecuteNonQuery();
                
                ////////////////////////////////////
                string Sql3 = "DELETE FROM Vehicles WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command3 = new(Sql3, dbConnection.Connection);
                Command3.Parameters.AddWithValue("@VehicleID", long.Parse(VehicleID));
                Command3.ExecuteNonQuery();
               

                transaction.Commit();
                Console.WriteLine($"Deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"Failed to delete: {ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        /// <summary>
        /// Deletes additional information of a vehicle from the database.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle whose additional information is to be deleted.</param>
        /// <returns>True if the additional information of the vehicle is successfully deleted from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the additional information of the vehicle from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL command to delete the additional information of the vehicle
        /// from the "VehiclesInformations" table based on the provided VehicleID. If the operation is successful, the method returns true.
        /// If an exception occurs during the process, the method returns false with an error message printed to the console.
        /// Finally, the database connection is closed.
        /// </remarks>
        public bool DeleteVehicleInformations(string VehicleID)
        {
            try
            {
                dbConnection.OpenConnection();

                ////////////////////////////////////
                string Sql = "DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID";

                using NpgsqlCommand Command = new(Sql, dbConnection.Connection);
                Command.Parameters.AddWithValue("@VehicleID", long.Parse(VehicleID));
                Command.ExecuteNonQuery();

                Console.WriteLine($"Deleted successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete: {ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves information about a vehicle from the database based on the provided vehicle ID.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle to retrieve information for.</param>
        /// <returns>A <see cref="Vehicle"/> object containing the information about the vehicle if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving information about the vehicle from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about the vehicle
        /// from the "Vehicles" table based on the provided VehicleID. If the vehicle is found, a new <see cref="Vehicle"/> object is created
        /// with the retrieved information and returned. If no matching vehicle is found, null is returned. If an exception occurs during the process,
        /// the method returns null with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public Vehicle? GetVehicle(string vehicleID)
        {
            try
            {
                dbConnection.OpenConnection();

                string sql = "SELECT * FROM Vehicles WHERE VehicleID = @VehicleID";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));

                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Vehicle vehicle = new()
                    {
                        VehicleID = reader.GetInt32(reader.GetOrdinal("VehicleID")),
                        VehicleNumber = reader.GetInt32(reader.GetOrdinal("VehicleNumber")),
                        VehicleType = reader.GetString(reader.GetOrdinal("VehicleType")),

                        //DriverID = reader.IsDBNull(reader.GetOrdinal("DriverID")) ? 0 : reader.GetInt32(reader.GetOrdinal("DriverID")),
                        //VehicleMake = reader.GetString(reader.GetOrdinal("VehicleMake")),
                        //VehicleModel = reader.GetString(reader.GetOrdinal("VehicleModel")),
                        //PurchaseDate = reader.GetInt32(reader.GetOrdinal("PurchaseDate")),
                    };

                    return vehicle;
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
        /// Retrieves additional information about a vehicle from the database based on the provided vehicle ID.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle to retrieve additional information for.</param>
        /// <returns>A <see cref="Vehicle"/> object containing the additional information about the vehicle if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving additional information about the vehicle from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve additional information about the vehicle
        /// from the "VehiclesInformations" table based on the provided VehicleID. If the vehicle is found, a new <see cref="Vehicle"/> object is created
        /// with the retrieved additional information and returned. If no matching vehicle is found, null is returned. If an exception occurs during the process,
        /// the method returns null with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public Vehicle? GetVehicleInformations(string vehicleID)
        {
            try
            {
                dbConnection.OpenConnection();

                string sql = "SELECT VehicleID, DriverID, VehicleMake, VehicleModel, PurchaseDate   FROM VehiclesInformations WHERE VehicleID = @VehicleID";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);
                command.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));

                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Vehicle vehicle = new()
                    {
                        VehicleID = reader.GetInt32(0),
                        //VehicleNumber = reader.GetInt32(reader.GetOrdinal("VehicleNumber")),
                        //VehicleType = reader.GetString(reader.GetOrdinal("VehicleType")),

                        DriverID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        VehicleMake = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        VehicleModel = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        PurchaseDate = reader.IsDBNull(4) ? 0 : reader.GetInt64(4),
                    };

                    return vehicle;
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
        /// Adds a route history record to the database.
        /// </summary>
        /// <param name="history">The <see cref="RouteHistory"/> object representing the route history to be added.</param>
        /// <returns>True if the route history record is successfully added to the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the route history record to the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and inserts the route history information into the "RouteHistory" table.
        /// If the operation is successful, the method returns true. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool AddRouteHistory(RouteHistory history)
        {

            try
            {
                dbConnection.OpenConnection();
                ///////////////////////////////////////
                string sql = "INSERT INTO RouteHistory(VehicleID, VehicleDirection, Status, VehicleSpeed, Epoch, Address, Latitude, Longitude)" +
                    " VALUES(@VehicleID, @VehicleDirection, @Status, @VehicleSpeed, @Epoch, @Address, @Latitude, @Longitude)";

                using NpgsqlCommand command = new(sql, dbConnection.Connection);

                command.Parameters.AddWithValue("@VehicleID", history.VehicleID);
                command.Parameters.AddWithValue("@VehicleDirection", history.VehicleDirection);
                command.Parameters.AddWithValue("@Status", history.Status);
                command.Parameters.AddWithValue("@VehicleSpeed", history.VehicleSpeed);
                command.Parameters.AddWithValue("@Epoch", history.Epoch);
                command.Parameters.AddWithValue("@Address", history.Address);
                command.Parameters.AddWithValue("@Latitude", history.Latitude);
                command.Parameters.AddWithValue("@Longitude", history.Longitude);

                command.ExecuteNonQuery();
                /////////////////////////////////////

                Console.WriteLine($"Route added successfully");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to add route {ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about all vehicles from the database along with their latest route history.
        /// </summary>
        /// <param name="vehicles">An out parameter that will contain the list of <see cref="Vehicle"/> objects representing the vehicles along with their latest route history.</param>
        /// <returns>True if the vehicle information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving vehicle information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about all vehicles
        /// from the "Vehicles" table along with their latest route history details. The retrieved information is stored in a list of <see cref="Vehicle"/> objects.
        /// If the operation is successful, the method returns true along with the list of vehicles. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllVehicles(out List<Vehicle> vehicles)
        {
            vehicles = [];
            try
            {
                dbConnection.OpenConnection();
                string sql1 = @"SELECT 
        Vehicles.VehicleID, 
        Vehicles.VehicleNumber, 
        Vehicles.VehicleType, 
        (
            SELECT VehicleDirection
            FROM RouteHistory
            WHERE RouteHistory.VehicleID = Vehicles.VehicleID
            ORDER BY Epoch DESC
            LIMIT 1
        ) AS LastDirection,
        (
            SELECT Status
            FROM RouteHistory
            WHERE RouteHistory.VehicleID = Vehicles.VehicleID
            ORDER BY Epoch DESC
            LIMIT 1
        ) AS LastStatus,
        (
            SELECT Address
            FROM RouteHistory
            WHERE RouteHistory.VehicleID = Vehicles.VehicleID
            ORDER BY Epoch DESC
            LIMIT 1
        ) AS LastAddress,
        (
            SELECT Latitude
            FROM RouteHistory
            WHERE RouteHistory.VehicleID = Vehicles.VehicleID
            ORDER BY Epoch DESC
            LIMIT 1
        ) AS LastLatitude,
        (
            SELECT Longitude
            FROM RouteHistory
            WHERE RouteHistory.VehicleID = Vehicles.VehicleID
            ORDER BY Epoch DESC
            LIMIT 1
        ) AS LastLongitude
    FROM Vehicles
    ORDER BY Vehicles.VehicleID; ";
                
                using NpgsqlCommand command1 = new(sql1, dbConnection.Connection);
                using NpgsqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    Vehicle vehicle = new()
                    {
                        VehicleID = reader1.GetInt64(0),
                        VehicleNumber = reader1.GetInt64(1),
                        VehicleType = reader1.GetString(2),

                    };

                    vehicle.LastDirection = 
                        reader1.IsDBNull(reader1.GetOrdinal("LastDirection")) ? 0 : reader1.GetInt32(reader1.GetOrdinal("LastDirection"));

                    vehicle.LastStatus = 
                        reader1.IsDBNull(reader1.GetOrdinal("LastStatus")) ? '0' : reader1.GetChar(reader1.GetOrdinal("LastStatus"));
                    
                    vehicle.LastAddress = 
                        reader1.IsDBNull(reader1.GetOrdinal("LastAddress")) ? string.Empty : reader1.GetString(reader1.GetOrdinal("LastAddress"));
                    
                    vehicle.LastLatitude = 
                        reader1.IsDBNull(reader1.GetOrdinal("LastLatitude")) ? 0 : reader1.GetDouble(reader1.GetOrdinal("LastLatitude"));
                    
                    vehicle.LastLongitude = 
                        reader1.IsDBNull(reader1.GetOrdinal("LastLongitude")) ? 0 : reader1.GetDouble(reader1.GetOrdinal("LastLongitude"));

                    vehicles.Add(vehicle);

                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally {
                dbConnection.CloseConnection(); 
            }
        }

        /// <summary>
        /// Retrieves detailed information about a vehicle and its associated driver from the database based on the provided vehicle ID.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle for which information is to be retrieved.</param>
        /// <param name="vehicle">An out parameter that will contain the <see cref="Vehicle"/> object representing the vehicle information.</param>
        /// <param name="driver">An out parameter that will contain the <see cref="Driver"/> object representing the driver information associated with the vehicle.</param>
        /// <returns>True if the vehicle and driver information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving vehicle and driver information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, begins a transaction, and then executes multiple SQL queries to retrieve detailed information
        /// about the vehicle and its associated driver from the respective tables. The retrieved information is stored in <see cref="Vehicle"/> and <see cref="Driver"/> objects.
        /// If the operation is successful, the method returns true along with the populated vehicle and driver objects. If an exception occurs during the process,
        /// the transaction is rolled back, and the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GettheVehicleInformations(string vehicleID, out Vehicle vehicle, out Driver driver)
        {
            NpgsqlTransaction? transaction = null;
            vehicle = new Vehicle();
            driver = new Driver();

            try
            {
                dbConnection.OpenConnection();
                transaction = dbConnection.Connection.BeginTransaction();

                ///////////////////////////////////////
                string sql1 = "SELECT VehicleNumber, VehicleType FROM Vehicles WHERE VehicleID = @VehicleID;";
               
                using NpgsqlCommand command1 = new(sql1, dbConnection.Connection);
                command1.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
                using NpgsqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.Read())
                {
                    vehicle.VehicleID = long.Parse(vehicleID);
                    vehicle.VehicleNumber = reader1.IsDBNull(0) ? 0 : reader1.GetInt64(0);
                    vehicle.VehicleType = reader1.IsDBNull(1) ? string.Empty : reader1.GetString(1);
                }     
                reader1.Close();

                ///////////////////////////////////////
                //get vehicle info
                string sql2 = "SELECT VehicleMake, VehicleModel, DriverID FROM VehiclesInformations WHERE VehicleID = @VehicleID;";

                using NpgsqlCommand command2 = new(sql2, dbConnection.Connection);
                command2.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
                using NpgsqlDataReader reader2 = command2.ExecuteReader();

                if (reader2.Read())
                {
                    vehicle.VehicleMake = reader2.IsDBNull(0) ? string.Empty : reader2.GetString(0);
                    vehicle.VehicleModel = reader2.IsDBNull(1) ? string.Empty : reader2.GetString(1);
                    vehicle.DriverID = reader2.IsDBNull(2) ? 0 : reader2.GetInt64(2);
                }
                reader2.Close();

                ///////////////////////////////////////
                //get driver information
                string sql3 = "SELECT DriverName, PhoneNumber FROM Driver WHERE DriverID = @DriverID;";
                using NpgsqlCommand command3 = new(sql3, dbConnection.Connection);
                command3.Parameters.AddWithValue("@DriverID", vehicle.DriverID);
                using NpgsqlDataReader reader3 = command3.ExecuteReader();

                if (reader3.Read())
                {
                    driver.DriverID = vehicle.DriverID;
                    driver.DriverName = reader3.IsDBNull(0) ? string.Empty: reader3.GetString(0);
                    driver.PhoneNumber = reader3.IsDBNull(1) ? 0 : reader3.GetInt64(1);

                }
                reader3.Close();

                ///////////////////////////////////////
                //get vehicle last route history
                string sql4 = "SELECT Latitude, Latitude, Epoch, VehicleSpeed, Address FROM RouteHistory WHERE VehicleID = @VehicleID ORDER BY Epoch DESC LIMIT 1;";
                using NpgsqlCommand command4 = new(sql4, dbConnection.Connection);
                command4.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
                using NpgsqlDataReader reader4 = command4.ExecuteReader();

                if (reader4.Read())
                {
                    vehicle.LastLatitude = reader4.IsDBNull(0) ? 0 :  reader4.GetDouble(0);
                    vehicle.LastLongitude = reader4.IsDBNull(1) ? 0 : reader4.GetDouble(1);
                    vehicle.LastGPSTime = reader4.IsDBNull(2) ? 0 : reader4.GetInt64(2);
                    vehicle.LastGPSSpeed = reader4.IsDBNull(3) ? string.Empty : reader4.GetString(3);
                    vehicle.LastAddress = reader4.IsDBNull(4) ? string.Empty : reader4.GetString(4);
                }
                reader4.Close();

                ///////////////////////////////////////
                transaction.Commit();
                return true;
                
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine($"error: {ex.Message}");
                return false;
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        /// <summary>
        /// Retrieves route history information for a vehicle from the database within the specified time range.
        /// </summary>
        /// <param name="vehicleID">The ID of the vehicle for which route history information is to be retrieved.</param>
        /// <param name="StartTime">The start time of the route history period (in epoch format).</param>
        /// <param name="EndTime">The end time of the route history period (in epoch format).</param>
        /// <param name="vehicle">An out parameter that will contain the <see cref="Vehicle"/> object representing the vehicle information.</param>
        /// <param name="histories">An out parameter that will contain the list of <see cref="RouteHistory"/> objects representing the route history information.</param>
        /// <returns>True if the route history information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving route history information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database, begins a transaction, and then executes an SQL query to retrieve route history information
        /// for the specified vehicle within the provided time range from the "RouteHistory" table. The retrieved information is stored in a list of <see cref="RouteHistory"/> objects.
        /// If the operation is successful, the method returns true along with the populated vehicle object and list of route history objects. If an exception occurs during the process,
        /// the transaction is rolled back, and the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetVehicleRouteHistory(string vehicleID, string StartTime, string EndTime, out Vehicle vehicle, out List<RouteHistory> histories)
        {
            NpgsqlTransaction? transaction = null;
            vehicle = new Vehicle();
            histories = [];

            try
            {
                dbConnection.OpenConnection();
                transaction = dbConnection.Connection.BeginTransaction();

                ///////////////////////////////////////
                string sql1 = "SELECT VehicleNumber FROM Vehicles WHERE VehicleID = @VehicleID;";

                using NpgsqlCommand command1 = new(sql1, dbConnection.Connection);
                command1.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
                using NpgsqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.Read())
                {
                    vehicle.VehicleID = long.Parse(vehicleID);
                    vehicle.VehicleNumber = reader1.IsDBNull(0) ? 0 : reader1.GetInt64(0);
                }
                reader1.Close();

                ///////////////////////////////////////
                //get vehicle routes
                string sql2 = "SELECT Address, Status, Latitude, Longitude, VehicleDirection, VehicleSpeed, Epoch  " +
                    "FROM RouteHistory WHERE VehicleID = @VehicleID AND Epoch >= @StartTime AND Epoch <= @EndTime ORDER BY Epoch DESC";

                using NpgsqlCommand command2 = new(sql2, dbConnection.Connection);
                command2.Parameters.AddWithValue("@VehicleID", long.Parse(vehicleID));
                command2.Parameters.AddWithValue("@StartTime", long.Parse(StartTime));
                command2.Parameters.AddWithValue("@EndTime", long.Parse(EndTime));
                using NpgsqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    RouteHistory routeHistory = new()
                    {
                        Address = reader2.IsDBNull(0) ? string.Empty : reader2.GetString(0),
                        Status = reader2.IsDBNull(1) ? '0' : reader2.GetChar(1),
                        Latitude = reader2.IsDBNull(2) ? 0 : reader2.GetDouble(2),
                        Longitude = reader2.IsDBNull(3) ? 0 : reader2.GetDouble(3),
                        VehicleDirection = reader2.IsDBNull(4) ? 0 : reader2.GetInt32(4),
                        VehicleSpeed = reader2.IsDBNull(5) ? string.Empty : reader2.GetString(5),
                        Epoch = reader2.IsDBNull(6) ? 0 : reader2.GetInt64(6),
                    };
                    histories.Add(routeHistory);
                }
                reader2.Close();

                ///////////////////////////////////////
                transaction.Commit();
                return true;

            }
            catch (Exception ex)
            {
                transaction?.Rollback();
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
