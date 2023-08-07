using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroupGeofence
    {
        public int Groupid { get; set; }
        public int Geofenceid { get; set; }
        public int Id { get; set; }

        public TcGeofences Geofence { get; set; }
        public TcGroups Group { get; set; }
    }
}
