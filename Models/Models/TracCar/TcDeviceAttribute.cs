using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDeviceAttribute
    {
        public int Deviceid { get; set; }
        public int Attributeid { get; set; }
        public int Id { get; set; }

        public TcAttributes Attribute { get; set; }
        public TcDevices Device { get; set; }
    }
}
