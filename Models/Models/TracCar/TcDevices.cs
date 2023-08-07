using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDevices
    {
        public TcDevices()
        {
            TcDeviceAttribute = new HashSet<TcDeviceAttribute>();
            TcDeviceCommand = new HashSet<TcDeviceCommand>();
            TcDeviceDriver = new HashSet<TcDeviceDriver>();
            TcDeviceGeofence = new HashSet<TcDeviceGeofence>();
            TcEvents = new HashSet<TcEvents>();
            TcPositions = new HashSet<TcPositions>();
            TcUserDevice = new HashSet<TcUserDevice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Uniqueid { get; set; }
        public DateTime? Lastupdate { get; set; }
        public int? Positionid { get; set; }
        public int? Groupid { get; set; }
        public string Attributes { get; set; }
        public string Phone { get; set; }
        public string Model { get; set; }
        public string Contact { get; set; }
        public string Category { get; set; }
        public bool? Disabled { get; set; }

        public TcGroups Group { get; set; }
        public ICollection<TcDeviceAttribute> TcDeviceAttribute { get; set; }
        public ICollection<TcDeviceCommand> TcDeviceCommand { get; set; }
        public ICollection<TcDeviceDriver> TcDeviceDriver { get; set; }
        public ICollection<TcDeviceGeofence> TcDeviceGeofence { get; set; }
        public ICollection<TcEvents> TcEvents { get; set; }
        public ICollection<TcPositions> TcPositions { get; set; }
        public ICollection<TcUserDevice> TcUserDevice { get; set; }
    }
}
