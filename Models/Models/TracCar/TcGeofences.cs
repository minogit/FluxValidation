using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGeofences
    {
        public TcGeofences()
        {
            TcDeviceGeofence = new HashSet<TcDeviceGeofence>();
            TcGroupGeofence = new HashSet<TcGroupGeofence>();
            TcUserGeofence = new HashSet<TcUserGeofence>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Attributes { get; set; }
        public int? Calendarid { get; set; }

        public TcCalendars Calendar { get; set; }
        public ICollection<TcDeviceGeofence> TcDeviceGeofence { get; set; }
        public ICollection<TcGroupGeofence> TcGroupGeofence { get; set; }
        public ICollection<TcUserGeofence> TcUserGeofence { get; set; }
    }
}
