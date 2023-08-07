using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcEvents
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Servertime { get; set; }
        public int? Deviceid { get; set; }
        public int? Positionid { get; set; }
        public int? Geofenceid { get; set; }
        public string Attributes { get; set; }
        public int? Maintenanceid { get; set; }

        public TcDevices Device { get; set; }
    }
}
