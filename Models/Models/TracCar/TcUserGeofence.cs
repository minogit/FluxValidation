using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserGeofence
    {
        public int Userid { get; set; }
        public int Geofenceid { get; set; }
        public int Id { get; set; }

        public TcGeofences Geofence { get; set; }
        public TcUsers User { get; set; }
    }
}
