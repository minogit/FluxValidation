using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcPositions
    {
        public int Id { get; set; }
        public string Protocol { get; set; }
        public int Deviceid { get; set; }
        public DateTime Servertime { get; set; }
        public DateTime Devicetime { get; set; }
        public DateTime Fixtime { get; set; }
        public bool Valid { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Speed { get; set; }
        public double Course { get; set; }
        public string Address { get; set; }
        public string Attributes { get; set; }
        public double Accuracy { get; set; }
        public string Network { get; set; }

        public TcDevices Device { get; set; }
    }
}
