using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetMSLogic.Entities;
using FPro;
using Npgsql;

namespace FleetMSLogic.Repository
{
    /// <summary>
    /// Represents a repository for managing Geofences in the database.
    /// </summary>
    public class GeofenceRepository
    {
        private readonly DatabaseConnection dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeofenceRepository"/> class.
        /// </summary>
        /// <remarks>Creates a new instance of <see cref="GeofenceRepository"/> and initializes the database connection.</remarks>
        public GeofenceRepository()
        {
            dbConnection = new DatabaseConnection();
        }

        /// <summary>
        /// Retrieves information about all geofences from the database.
        /// </summary>
        /// <param name="geofences">An out parameter that will contain the list of <see cref="Geofences"/> objects representing the geofence information.</param>
        /// <returns>True if the geofence information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving geofence information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about all geofences
        /// from the "Geofences" table. The retrieved information is stored in a list of <see cref="Geofences"/> objects.
        /// If the operation is successful, the method returns true along with the populated list of geofences. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllGeofencesInformations(out List<Geofences> geofences)
        {
            geofences = [];
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT GeofenceID, GeofenceType, AddedDate, StrockColor, StrockOpacity, StrockWeight, FillColor, FillOpacity " +
                    "FROM Geofences";
                using NpgsqlCommand command = new NpgsqlCommand(sql ,dbConnection.Connection);
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Geofences geofence = new()
                    {
                        GeofenceID = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                        GeofenceType = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        AddedDate = reader.IsDBNull(2) ? 0 : reader.GetInt64(2),
                        StrokeColor = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        StrokeOpacity = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                        StrokeWeight = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                        FillColor = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                        FillOpacity = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                    };

                    geofences.Add(geofence);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about all circular geofences from the database.
        /// </summary>
        /// <param name="geofences">An out parameter that will contain the list of <see cref="CircleGeofence"/> objects representing the circular geofence information.</param>
        /// <returns>True if the circular geofence information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving circular geofence information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about all circular geofences
        /// from the "CircleGeofence" table. The retrieved information is stored in a list of <see cref="CircleGeofence"/> objects.
        /// If the operation is successful, the method returns true along with the populated list of circular geofences. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllCircularGeofence(out List<CircleGeofence> geofences)
        {
            geofences = [];
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT GeofenceID, Radius, Latitude, Longitude " +
                    "FROM CircleGeofence";
                using NpgsqlCommand command = new NpgsqlCommand(sql, dbConnection.Connection);
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CircleGeofence geofence = new()
                    {
                        GeofenceID = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                        Radius = reader.IsDBNull(1) ? 0 : reader.GetInt64(1),
                        Latitude = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                        Longitude = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                    };

                    geofences.Add(geofence);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about all rectangular geofences from the database.
        /// </summary>
        /// <param name="geofences">An out parameter that will contain the list of <see cref="RectangleGeofence"/> objects representing the rectangular geofence information.</param>
        /// <returns>True if the rectangular geofence information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving rectangular geofence information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about all rectangular geofences
        /// from the "RectangleGeofence" table. The retrieved information is stored in a list of <see cref="RectangleGeofence"/> objects.
        /// If the operation is successful, the method returns true along with the populated list of rectangular geofences. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllRectangularGeofence(out List<RectangleGeofence> geofences)
        {
            geofences = [];
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT GeofenceID, North, East, West, South " +
                    "FROM RectangleGeofence";
                using NpgsqlCommand command = new NpgsqlCommand(sql, dbConnection.Connection);
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RectangleGeofence geofence = new()
                    {
                        GeofenceID = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                        North = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                        East = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                        West = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                        South = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                    };

                    geofences.Add(geofence);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }

        /// <summary>
        /// Retrieves information about all polygonal geofences from the database.
        /// </summary>
        /// <param name="geofences">An out parameter that will contain the list of <see cref="PolygonGeofence"/> objects representing the polygonal geofence information.</param>
        /// <returns>True if the polygonal geofence information is successfully retrieved from the database; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving polygonal geofence information from the database.</exception>
        /// <remarks>
        /// This method opens a connection to the database and executes an SQL query to retrieve information about all polygonal geofences
        /// from the "PolygonGeofence" table. The retrieved information is stored in a list of <see cref="PolygonGeofence"/> objects.
        /// If the operation is successful, the method returns true along with the populated list of polygonal geofences. If an exception occurs during the process,
        /// the method returns false with an error message printed to the console. Finally, the database connection is closed.
        /// </remarks>
        public bool GetAllPolygonGeofence(out List<PolygonGeofence> geofences)
        {
            geofences = [];
            try
            {
                dbConnection.OpenConnection();
                string sql = "SELECT GeofenceID, latitude, longitude " +
                    "FROM PolygonGeofence";
                using NpgsqlCommand command = new NpgsqlCommand(sql, dbConnection.Connection);
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PolygonGeofence geofence = new()
                    {
                        GeofenceID = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                        Latitude = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                        Longitude = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),

                    };

                    geofences.Add(geofence);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
            finally { dbConnection.CloseConnection(); }
        }
    }
}
