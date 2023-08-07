using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDeviceGeofence
    {
        public int Deviceid { get; set; }
        public int Geofenceid { get; set; }
        public int Id { get; set; }

        public TcDevices Device { get; set; }
        public TcGeofences Geofence { get; set; }
    }
}
