using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using Autodesk.Revit.DB;
using System.Net;
using RevitAddinBootcamp.Common;
using System.Windows.Controls;
using System.Security.Cryptography.X509Certificates;

namespace RevitAddinBootcamp
{

    public class MovingList
    {
        public List<FurnitureInstance> FurnitureList { get; set; }
        public class FurnitureInstance
        {
            public string RoomName { get; set; }
            public string FamilyName { get; set; }
            public string TypeName { get; set; }
            public int Count { get; set; }
            public FurnitureInstance(string _roomName, string _familyName, string _familyTypeName, int count)
            {
                RoomName = _roomName;
                FamilyName = _familyName;
                TypeName = _familyTypeName;
                Count = count;
            }
            public string GetRoomName()
            {
                return RoomName;
            }
            public string GetFamilyName()
            {
                return FamilyName;

            }
            public string GetFamilyTypeName()
            {
                return TypeName;
            }
            public int GetQuantity()
            {
                return Count;
            }
        }
    }

// 4. create another class
public class Neighborhood
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<Building> BuildingList { get; set; }

        public Neighborhood(string _name, string _city, string _state,
            List<Building> _buildingList)
        {
            Name = _name;
            City = _city;
            State = _state;
            BuildingList = _buildingList;
        }

        public int GetBuildingCount()
        {
            return BuildingList.Count;
        }
    }

    // 1. create class 
    public class Building
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfFloors { get; set; }
        public double Area { get; set; }

        // 3. add constuctor to class
        public Building(string _name, string _address, int _numberOfFloors, double _area)
        {
            Name = _name;
            Address = _address;
            NumberOfFloors = _numberOfFloors;
            Area = _area;
        }


    }
}

