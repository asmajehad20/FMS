using FleetMSLogic.Entities;
using FleetMSLogic.Repository;
using FPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetMSLogic.Manager
{
    public enum GeofencesTags
    {
        GeofenceID,
        GeofenceType,
        AddedDate,
        StrokeColor,
        StrokeOpacity,
        StrokeWeight,
        FillColor,
        FillOpacity,

        Radius,
        Latitude,
        Longitude,

        North,
        East,
        West,
        South

    }


    public class GeofenceManager
    {
        private readonly GeofenceRepository _geofenceRepository;
        public GeofenceManager() 
        {
            _geofenceRepository = new GeofenceRepository();
        }

        public GVAR GetAllGeofences()
        {
            //get data from repository;
            if (!_geofenceRepository.GetAllGeofencesInformations(out List<Geofences> geofences))
            {
                throw new Exception($"Failed to get geofences");
            }

            //conver them to dataTable
            var GeofencesDataTable = new DataTable();

            GeofencesDataTable.Columns.Add(GeofencesTags.GeofenceID.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.GeofenceType.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.AddedDate.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.StrokeColor.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.StrokeOpacity.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.StrokeWeight.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.FillColor.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.FillOpacity.ToString());

            foreach (var geofence in geofences)
            {
                DataRow row = GeofencesDataTable.NewRow();
                row[GeofencesTags.GeofenceID.ToString()] = geofence.GeofenceID;
                row[GeofencesTags.GeofenceType.ToString()] = geofence.GeofenceType;
                row[GeofencesTags.AddedDate.ToString()] = geofence.AddedDate;
                row[GeofencesTags.StrokeColor.ToString()] = geofence.StrokeColor;
                row[GeofencesTags.StrokeOpacity.ToString()] = geofence.StrokeOpacity;
                row[GeofencesTags.StrokeWeight.ToString()] = geofence.StrokeWeight;
                row[GeofencesTags.FillColor.ToString()] = geofence.FillColor;
                row[GeofencesTags.FillOpacity.ToString()] = geofence.FillOpacity;

                GeofencesDataTable.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT["Geofences"] = GeofencesDataTable;
            return Gvar;
        }

        public GVAR GetAllCircularGeofence()
        {
            //get data from repository;
            if (!_geofenceRepository.GetAllCircularGeofence(out List<CircleGeofence> geofences))
            {
                throw new Exception($"Failed to get geofences");
            }

            //conver them to dataTable
            var GeofencesDataTable = new DataTable();

            GeofencesDataTable.Columns.Add(GeofencesTags.GeofenceID.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.Radius.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.Latitude.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.Longitude.ToString());
            

            foreach (var geofence in geofences)
            {
                DataRow row = GeofencesDataTable.NewRow();
                row[GeofencesTags.GeofenceID.ToString()] = geofence.GeofenceID;
                row[GeofencesTags.Radius.ToString()] = geofence.Radius;
                row[GeofencesTags.Latitude.ToString()] = geofence.Latitude;
                row[GeofencesTags.Longitude.ToString()] = geofence.Longitude;
                

                GeofencesDataTable.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT["CircularGeofences"] = GeofencesDataTable;
            return Gvar;
        }

        public GVAR GetAllRectangularGeofence()
        {
            //get data from repository;
            if (!_geofenceRepository.GetAllRectangularGeofence(out List<RectangleGeofence> geofences))
            {
                throw new Exception($"Failed to get geofences");
            }

            //conver them to dataTable
            var GeofencesDataTable = new DataTable();

            GeofencesDataTable.Columns.Add(GeofencesTags.GeofenceID.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.North.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.East.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.West.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.South.ToString());

            foreach (var geofence in geofences)
            {
                DataRow row = GeofencesDataTable.NewRow();
                row[GeofencesTags.GeofenceID.ToString()] = geofence.GeofenceID;
                row[GeofencesTags.North.ToString()] = geofence.North;
                row[GeofencesTags.East.ToString()] = geofence.East;
                row[GeofencesTags.West.ToString()] = geofence.West;
                row[GeofencesTags.South.ToString()] = geofence.South;

                GeofencesDataTable.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT["RectangularGeofences"] = GeofencesDataTable;
            return Gvar;
        }

        public GVAR GetAllPolygonGeofence()
        {
            //get data from repository;
            if (!_geofenceRepository.GetAllPolygonGeofence(out List<PolygonGeofence> geofences))
            {
                throw new Exception($"Failed to get geofences");
            }

            //conver them to dataTable
            var GeofencesDataTable = new DataTable();

            GeofencesDataTable.Columns.Add(GeofencesTags.GeofenceID.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.Latitude.ToString());
            GeofencesDataTable.Columns.Add(GeofencesTags.Longitude.ToString());


            foreach (var geofence in geofences)
            {
                DataRow row = GeofencesDataTable.NewRow();
                row[GeofencesTags.GeofenceID.ToString()] = geofence.GeofenceID;
                row[GeofencesTags.Latitude.ToString()] = geofence.Latitude;
                row[GeofencesTags.Longitude.ToString()] = geofence.Longitude;


                GeofencesDataTable.Rows.Add(row);
            }

            //convert to gver
            GVAR Gvar = new();
            Gvar.DicOfDT["PolygonGeofences"] = GeofencesDataTable;
            return Gvar;
        }
    }
}
