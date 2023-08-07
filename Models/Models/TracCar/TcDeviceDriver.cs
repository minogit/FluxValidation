using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcDeviceDriver
    {
        public int Deviceid { get; set; }
        public int Driverid { get; set; }
        public int Id { get; set; }

        public TcDevices Device { get; set; }
        public TcDrivers Driver { get; set; }
    }
}
